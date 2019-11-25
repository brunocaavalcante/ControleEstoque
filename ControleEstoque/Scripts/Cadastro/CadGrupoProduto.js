function add_anti_forgery_token(data) { //Pega os dados(param) enviados nos posts abaixo e adiciona mais um parametro que é o token de verificação criado na linha 74 assim o controle valida se é um usuario valido ou não
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken]').val();
    return data;
}

function abrir_form(dados) {
    $('#id_cadastro').val(dados.Id); //Preenche os campos do form
    $('#txt_nome').val(dados.Nome);
    $('#cbx_ativo').prop('checked', dados.Ativo);
    var modal_cadastro = $('#modal_cadastro');
    bootbox.dialog({  // Cria um alert com bootbox com os campos acima
        title: titulo,
        message: modal_cadastro
    })
        .on('shown.bs.modal', function () {
            modal_cadastro.show(0, function () {
                $('#txt_nome').focus();
            });
        })
        .on('hidden.bs.modal', function () {
            modal_cadastro.hide().appendTo('body');
        });
}

function criar_linha_grid(dados) {
    var ret =
        '<tr data-id=' + dados.Id + '>' +
        '<td>' + dados.Nome + '</td>' +
        '<td>' + (dados.Ativo ? 'SIM' : 'NÃO') + '</td>' +
        '<td>' +
        '<a class="btn btn-primary btn-alterar" role="button" style="margin-right: 3px"><i class="glyphicon glyphicon-pencil"></i> Alterar</a>' +
        '<a class="btn btn-danger btn-excluir" role="button"><i class="glyphicon glyphicon-trash"></i> Excluir</a>' +
        '</td>' +
        '</tr>';
    return ret;
}

$(document).on('click', "#btn_incluir", function () {
    $('#msg_aviso').hide();
    $('#msg_error').hide();
    $('#msg_error_aviso').hide();
    abrir_form({ Id: 0, Nome: '', Ativo: true });
})

$(document).on('click', '.btn-alterar', function () {
    $('#msg_aviso').hide();
    $('#msg_error').hide();
    $('#msg_error_aviso').hide();
    var btn = $(this),
        id = btn.closest('tr').attr('data-id'),
        url = urlRecuperarGrupoProduto,
        param = { 'id': id };
    $.post(url, add_anti_forgery_token(param), function (response) {
        if (response) {
            abrir_form(response);
        }
    });
})

$(document).on('click', '.btn-excluir', function () {
    var btn = $(this),
        tr = btn.closest('tr'),
        id = tr.attr('data-id'),
        url = urlExcluirGrupoProduto,
        param = { 'id': id };
    bootbox.confirm({
        message: "Realmente deseja excluir o grupo de produto?",
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-danger'
            },
            cancel: {
                label: 'Não',
                className: 'btn-success'
            }
        },
        callback: function (result) {
            if (result) {
                $.post(url, add_anti_forgery_token(param), function (response) {
                    if (response) {
                        tr.remove();
                    }
                });
            }
        }
    });
})

$(document)
    .on('click', '#btn_confirmar', function () {
        var btn = $(this),
            url = urlSalvarGrupoProduto,
            param = {
                Id: $('#id_cadastro').val(),
                Nome: $('#txt_nome').val(),
                Ativo: $('#cbx_ativo').prop('checked')
            };
        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response.Resultado == "OK") {
                if (param.Id == 0) {
                    param.Id = response.IdSalvo;
                    var table = $('#grid_cadastro').find('tbody'),
                        linha = criar_linha_grid(param); //aqui estamos adicionando uma nova linha na tabela
                    table.append(linha);
                }
                else {
                    //Aqui faz o update
                    var linha = $('#grid_cadastro').find('tr[data-id=' + param.Id + ']').find('td'); // -> Encontra a linha que sera feita a alteração
                    //Aqui estamos alterando os conteudos da tabela
                    linha
                        .eq(0).html(param.Nome).end() // se a coluna for igual a 0 preenchemos o html com o parametro nome
                        .eq(1).html(param.Ativo ? 'SIM' : 'NÃO');// se a coluna for igual a 1 preenchemos com o status
                }
                $('#modal_cadastro').parents('.bootbox').modal('hide');
            }
            else if (response.Resultado == "ERRO") {
                $('#msg_aviso').hide();
                $('#msg_error').show();
                $('#msg_error_aviso').hide();
            }
            else if (response.Resultado == "AVISO") {
                $('#msg_error_aviso').empty();
                $('#msg_error_aviso').append(formatar_msg_aviso(response.Mensagens));//Adicionando a msg de erro a div msg_aviso e chama a função para coloca a msg no formato certo para não ficar o json feio
                $('#msg_aviso').show();
                $('#msg_error').hide();
                $('#msg_error_aviso').show();
            }
        });
    })
    .on('click', '.page-item', function () {  //assim pegamos a class da pages na linha 86
        var btn = $(this), //pagando o botão que foi clicado
            pagina = btn.text(),
            tamPag = $('#ddl_tam_pag').val(),
            param = { 'pagina': pagina, 'tamPag': tamPag };

        var url = urlGrupoProdutoPagina;
        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response) {
                var table = $('#grid_cadastro').find('tbody');
                table.empty();
                for (var i = 0; i < response.length; i++) {
                    table.append(criar_linha_grid(response[i]));
                }
                btn.siblings().removeClass('active');
                btn.addClass('active');
            }
        })
    })
    .on('change', '#ddl_tam_pag', function () {
        var ddl = $(this),
            tamPag = ddl.val(),
            pagina = 1,
            url = urlGrupoProdutoPagina,
            param = { 'pagina': pagina, 'tamPag': tamPag };
        $.post(url, add_anti_forgery_token(param), function (response) {
            if (response) {
                var table = $('#grid_cadastro').find('tbody');
                table.empty();
                for (var i = 0; i < response.length; i++) {
                    table.append(criar_linha_grid(response[i]));
                }
            }
        });
    });


function formatar_msg_aviso(msg) {
    var resp = '';

    for (let i = 0; i < msg.length; i++) {
        resp = '<li>' + msg[i] + '</li>';
    }
    return '<ul>' + resp + '</ul>';
}