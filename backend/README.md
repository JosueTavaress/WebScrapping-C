# API

Esta API permite listar, informações de comidas e seus detalhes.

## Acesso à Documentação da API

Você pode acessar a documentação da API através do Swagger UI. Siga as instruções abaixo para acessar:

1. **Inicie o Servidor**: Certifique-se de que o servidor da API está em execução. Se não estiver, você pode seguir a documentação em [doc](/README.md).

2. **Acesse o Swagger UI**: Abra seu navegador e vá para o seguinte endereço: https://localhost:7289/swagger/index.html

3. **Explore a Documentação**: Na interface do Swagger UI, você pode ver todos os endpoints disponíveis, os parâmetros necessários, os modelos de dados e até mesmo testar os endpoints diretamente na interface.

## Exemplos de Uso Manualmente

Aqui está um exemplo de como utilizar os endpoints da API:

### 1. Listar

- **Endpoint**: GET /Foods/{skip}/take/{take}
- **Descrição**: Retorna uma lista de todos as comidas por pagina.
- **Exemplo de Requisição**:

![alt text](image.png)

## Script Scrapping 

- Assim que a aplicação estiver rodando, será automaticamente iniciada, em uma segunda thread a raspagem de dados. O script só varrerá o site caso o banco de dados não tenha nenhum dado salvo. Após salvar os dados, o script verifica se o banco está populado para não realizar a raspagem de dados novamente.