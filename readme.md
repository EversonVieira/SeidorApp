-- COMO RODAR A APLICAÇÃO

    -- DEPENDÊNCIAS
        - NPM 8.1.
        - .NET 6
        - Visual Studio 2022 (pois vem com .NET 6)
        - ANGULAR CLI 13.1.4 ou superior

        Semi-Dependência
        - DOCKER OBS: O docker foi inicialmente adicionado ao projeto .NET, mas não foi utilizado por contra tempos e problemas que surgiram de última hora.
          portanto é de extrema importância que o avaliador saiba que, pode ser que o Visual Studio peça o Docker, mesmo que não seja utilizado.

    -- BACKEND

        Abrir o arquivo .SLN
        No botão/dropdown com o símbolo de play verde, confirme que o nome é SeidorAPP (Vai rodar a aplicação com o console e sem docker). E marque ele.
        Ao executar a aplicação tenha certeza que a porta da api é a porta: 7213, caso não seja, apenass garanta que na Aplicação Web, no Arquivo
        HttpService.ts a base url utiliza a porta da API.
        Se quiser consumir a API via Insomnia ou POSTMAN, verifique os cabeçalhos (você pode achar eles em program.cs e em HttpService.ts na aplicação Web)
        Tenha certeza também de utilizar HTTPS e não HTTP.

    -- FRONT END

        Na raiz do projeto, procure pela pasta SeidorWebApp e abra um terminal nela
        Aberto o terminal, execute o comando NPM i e depois NG S, eles vão instalar as dependências e executar a aplicação.
        
    -- ATENÇÃO

        Caso as portas estejam ocupadas com outras aplicações, pode haver erros ou troca das portas automáticamente, por isso
        VERIFIQUE as portas caso tenha algum erro diferente do normal.

        Em caso de alguma dúvida entrar em contato pelo WhatsApp ou por chamada com o número: (81) 9 8919 9827

-- SOBRE A APLICAÇÃO

    - Todo o código fonte é escrito em inglês, mas as mensagens e o texto html foi escrito em português, isso foi de propósito.
    
-- CAMADAS

-- BACKEND
 
    - CONTROLLER

            Essa camada vai utilizar os métodos HTTP para receber as requisições em determinados métodos, e cada método
            vai chamar seu respectivo Business/Adapter e executar a operação, dependendo do objeto. 
          

    - ADAPTER

            Adapters vão adaptar o request para algo a nível da camada de Business, caso necessário, um bom exemplo é o
            Caso dos usuários, é possível ver o uso dos adapters na prática, por questões de modelagem, o
            Adapter recebe modelos diferentes, que herdam de outras classes, nesse caso da User e passam esses valores já tratados para o Business,
            Dessa forma, o Business só realiza operações referentes ao seu respectivo modelo.

    - BUSINESS

            Camadas de negócio, essa camada tem atributos privados referentes ao repositório e validadores,
            essa camada pode utilizar N Repositórios e N Validadores, é responsável por garantir a integridade do projeto.

    - VALIDATOR

            Camada que vai abstrair as validações de forma que o Business ou Adapter consigam utilizar de forma consistente, mantendo os códigos separados 
            e sua responsabilidade bem definida, bem como garantindo a reutilização dos métodos.

    - REPOSITORY

            Camada de Abstração do banco de dados, é aqui onde os Selects vão ser formados e construídos. 
            OBS: Todas as abstrações são utilizando o ADO.NET e todos os métodos que constroem os filtros dinamicamente, utilizando os objetos requests, foram
            implementados e criados por mim.

    

    -- UTILITY

            Utilitários que serão utilizados ao longo da aplicação.

    -- DataBase

            Conjunto de classes que vão atuar diretamente no banco de dados, como a 'SqliteDataBaseIntegrityService' que vai garantir a integridade 
            da Base de Dados

    -- Models

            Modelos que serão usados para passar dados entre as camadas e comunicar com o Front-End


-- FRONTEND

    Components: Vão ser os Views da aplicação,

    Services: Camadas que vão consumir a API, devem ser chamados nos Components

    Models: Modelos que serão usados para passar dados entre as camadas e comunicar com o BackEnd


-- TESTING

    Executar testes unitários, cumprindo um dos requisitos do projeto.

-- DOCKER

    Devido a alguns contra tempos, optei por não gastar tempo e recursos na sua implementação, mas o projeto .NET consta com um DockerFile,
    Que de fato pode gerar uma imagem, mas como não foi configurado adequadamente não é recomendável utilizar o DOCKER.