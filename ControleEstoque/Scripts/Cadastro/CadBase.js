function add_anti_forgery_token(data) { //Pega os dados(param) enviados nos posts abaixo e adiciona mais um parametro que é o token de verificação criado na linha 74 assim o controle valida se é um usuario valido ou não
    data.__RequestVerificationToken = $('[name=__RequestVerificationToken]').val();
    return data;
}

function formatar_msg_aviso(msg) {
    var resp = '';

    for (let i = 0; i < msg.length; i++) {
        resp = '<li>' + msg[i] + '</li>';
    }
    return '<ul>' + resp + '</ul>';
}

function abrir_form(dados) {
    set_dados_form(dados);
   
    var modal_cadastro = $('#modal_cadastro');

    bootbox.dialog({  // Cria um alert com bootbox com os campos acima
        title: 'Cadastro de '+tituloPagina,
        message: modal_cadastro
    })
    .on('shown.bs.modal', function () {
        modal_cadastro.show(0, function () {
            set_focus_form();
        });
    })
    .on('hidden.bs.modal', function () {
        modal_cadastro.hide().appendTo('body');
    });
}

$(document).on('click', "#btn_incluir", function () {
    $('#msg_aviso').hide();
    $('#msg_error').hide();
    $('#msg_error_aviso').hide();
    abrir_form(get_dados_inclusao());
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
        url = urlDeleteGrupoProduto,
        param = { 'id': id };
    bootbox.confirm({
        message: "Realmente deseja excluir o grupo de produto?",
        buttons: {
            confirm: {
                label: 'Sim',
                className: 'btn-success'
            },
            cancel: {
                label: 'Não',
                className: 'btn-danger'
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

$(document).on('click', '#btn_confirmar', function () {
    var btn = $(this),
        url = urlSalvarGrupoProduto,
        param = get_dados_form()
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
                preencher_linha_grid(linha,param)
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
            url = urlGrupoProdutoPagina,
            param = { 'pagina': pagina, 'tamPag': tamPag };
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
        var ddl = $(this), //Pegando o numero de linhas selecionado pelo usuario no dropdow-list linha 41
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
                ddl.siblings().removeClass('active');
                ddl.addClass('active');
            }
        })
    })

