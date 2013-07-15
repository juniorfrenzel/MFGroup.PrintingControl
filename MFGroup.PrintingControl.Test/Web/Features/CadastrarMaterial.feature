Funcionalidade: 
CadastrarMaterial
	Afim de garantir o funcionamento correto da tela de cadastro de material devemos realizar as seguintes validações.

Cenario: Ao tentar cadastrar um material com os campos em branco
	Dado que eu navegue até a pagina de Login
	E preencha os campos de loing com dados validos
	E clique no botao Login
	Dado que eu clique no menu Cadastrar Material
	E informe os campos 
	| id         | value |
	| Descricao  |       |
	| Marca      |       |
	| Quantidade |       |
	| Gramatura  |       |
	| Tamanho    |       |
	| Valor      |       |
	Quando clico no botao Salvar
	Entao as seguintes mensagens de erro devem ser exibidas
	| id        | value                    |
	| Descricao | Descrição não informada! |
	| Marca     | Marca não informada!     |

Cenario: Ao tentar cadastrar um material com o valores negativos de quantidade e valor
	Dado que eu navegue até a pagina de Login
	E preencha os campos de loing com dados validos
	E clique no botao Login
	Dado que eu clique no menu Cadastrar Material
	E informe os campos 
	| id         | value			|
	| Descricao  | Material 1		|
	| Quantidade | -1				|
	| Valor      | -1				|
	E selecione a marca
	| id    | value  |
	| Marca | MarcaA |
	Quando clico no botao Salvar
	Entao as seguintes mensagens de erro devem ser exibidas
	| id			| value															|
	| Quantidade	| Não é permitido cadastrar material com quantidade negativa!	|
	| Valor			| Não é permitido cadastrar material com valor negativo!		|