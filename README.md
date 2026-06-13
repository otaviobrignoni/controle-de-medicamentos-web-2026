# Controle de Medicamentos Web

**Trabalho 04 - [Academia do Programador](https://www.academiadoprogramador.net/inicio) 2026**

## Funcionalidades

- Cadastro, edição, exclusão e visualização de **fornecedores**
- Cadastro, edição, exclusão e visualização de **funcionários**
- Cadastro, edição, exclusão e visualização de **pacientes**
- Cadastro, edição, exclusão, detalhes e visualização de **medicamentos**
  - Vínculo com fornecedor
  - Controle de quantidade em estoque
- Controle de **estoque**:
  - Requisição de entrada: registra o recebimento de um medicamento por um funcionário
  - Requisição de saída: registra a entrega de um ou mais medicamentos a um paciente
  - Listagem separada de entradas e saídas

## Regras de Negócio

- Não é permitido cadastrar dois fornecedores com o mesmo CNPJ
- Não é permitido cadastrar dois funcionários com o mesmo CPF
- Não é permitido cadastrar dois pacientes com o mesmo Cartão SUS
- Não é permitido cadastrar dois medicamentos com o mesmo nome
- Uma requisição de saída deve conter pelo menos um medicamento
- A quantidade solicitada na saída não pode exceder o estoque disponível do medicamento

## Persistência de Dados

Os dados são armazenados em arquivo JSON no caminho:

`%LocalAppData%/ControleDeMedicamentosWeb/dados.json`

## Como Executar

### Pré-requisitos

- [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

### Passos

1. Abra a pasta do repositório.
2. Restaure e compile (opcional):

```bash
dotnet build ControleDeMedicamentos.slnx
```

3. Execute a aplicação:

```bash
dotnet run --project ControleDeMedicamentos.WebApp
```
