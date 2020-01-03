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

function set_dados_form(dados) {
    $('#id_cadastro').val(dados.Id); //Preenche os campos do form
    $('#txt_nome').val(dados.Nome);
    $('#cbx_ativo').prop('checked', dados.Ativo);
}

function set_focus_form() {
    $('#txt_nome').focus();
}

function get_dados_inclusao() {
    return { Id: 0, Nome: '', Ativo: true };
}

function get_dados_form() {
    return {
        Id: $('#id_cadastro').val(),
        Nome: $('#txt_nome').val(),
        Ativo: $('#cbx_ativo').prop('checked')
    };
}

function preencher_linha_grid(linha,param) {
    //Aqui estamos alterando os conteudos da tabela
    linha
        .eq(0).html(param.Nome).end() // se a coluna for igual a 0 preenchemos o html com o parametro nome
        .eq(1).html(param.Ativo ? 'SIM' : 'NÃO');// se a coluna for igual a 1 preenchemos com o status
}

