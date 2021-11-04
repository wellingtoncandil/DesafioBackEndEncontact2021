
# Teste back-end enContact

Bem-vindo ao teste para desenvolvimento back-end na enContact.

## O teste

Criamos um pequeno projeto onde tentamos simular o que acontece no dia a dia. Um código onde você poderá encontrar pontos de bug, refatoração, má performance etc. O projeto consiste em uma API simples para cadastro e consulta de contatos em uma agenda. 

Neste teste você poderá mostrar suas habilidades em c# e .net core, POO, lógica de programação, SQL, refatoração, e também suas habilidades de debug para encontrar problemas.

O foco deste teste é a implementação dos recursos necessários para atender os seguintes requisitos: 

	- Poder criar, editar, excluir e listar agendas.
	- Poder criar, editar, excluir e listar empresas.
	- Poder importar um ou mais contatos a partir de um .csv
		- Fique a vontade para definir o leiaute do arquivo.
		- Caso dê erro na importação de um registro, não deve impactar a importação dos demais.
		- Só deve ser possível cadastrar um contato vinculado a uma empresa (caso seja informada) e agenda pré-cadastrado.
		- O contato não precisa estar vinculado a uma empresa.
		- O contato precisa estar vinculado a uma agenda.
	- Poder pesquisar contatos.
		- Por qualquer campo do contato (incluído o nome da empresa).
		- A pesquisa deve ser paginada (Fique a vontade para utilizar qualquer estratégia).
	- Poder consultar os contatos de uma agenda e que estejam em uma determinada empresa.

	
## O repositório

1. Faça o fork do nosso repositório no Github.
2. Clone do projeto.
3. Faça as devidas alterações que achar necessário.
4. Caso seja necessário, enviar junto o arquivo "Database.db" que fica na raiz do projeto.

## O que fazer?

1. Crie/Edite as APIs necessárias para atender os requisitos.
2. Refatore o código da maneira que achar melhor.
3. Fique a vontade para usar qualquer biblioteca externa, caso seja necessário.


## Desafio do desafio

Tem um tempinho a mais? Acha que pode fazer mais? Então aqui vai alguns desafios para seu projeto, que serve como um plus no seu teste!

- Seria uma boa se pontos críticos do código tivessem testes unitários.
- Adicionar autenticação na API seria interessante.
- Poder exportar a agenda completa também seria legal.
