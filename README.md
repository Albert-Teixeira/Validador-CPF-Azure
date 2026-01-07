# Validador de CPF - Azure Functions (.NET 8)

Este projeto foi desenvolvido como parte de um desafio prático na plataforma **DIO (Digital Innovation One)**. O objetivo foi criar uma solução Serverless utilizando **Azure Functions** para validar CPFs, realizando todo o ciclo desde o desenvolvimento até o **deploy na nuvem**.

## Tecnologias Utilizadas

* **C# (.NET 8.0)** - Modelo de processo isolado.
* **Azure Functions** - Tecnologia Serverless para execução do código.
* **Azure East US 2** - Região de implantação utilizada para conformidade com as políticas da assinatura.
* **VS Code** - IDE utilizada para desenvolvimento e deploy.

## Funcionamento da API

A função está publicada e operacional na Azure. Ela utiliza um gatilho HTTP para processar as requisições.

* **Endpoint de Produção:** `https://dioproazure204.azurewebsites.net/api/fnvalidacpf`
* **Método aceito:** `POST`
* **Parâmetro esperado:** `cpf` (enviado via Query String ou corpo do JSON).
* **Resposta:** Confirmação se o formato e os dígitos verificadores do CPF são válidos.

## Segurança e Acesso

Para fins de certificação e avaliação:
1. O nível de autorização definido é **`Function`**, o que exige uma chave de acesso para execução.
2. **Atenção:** Por segurança, a chave de acesso (`code`) não está exposta neste repositório público. 
3. O link completo e funcional (URL + Chave) foi enviado exclusivamente através da plataforma de entrega da **DIO** para uso dos avaliadores.

## Estrutura de Arquivos

* `httpValidaCpf/`: Código fonte da função de validação.
* `host.json`: Configurações de runtime da Azure.
* `.gitignore`: Configurado para proteger arquivos sensíveis como `local.settings.json`.
