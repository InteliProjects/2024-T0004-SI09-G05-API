# 8. API de Integração

## 8.1 Introdução

Este projeto de API foi desenvolvido seguindo os princípios da Clean Architecture, visando a separação de interesses, a independência de frameworks e a facilidade de manutenção e testabilidade. A estrutura do projeto é organizada em camadas distintas, com a lógica de negócios centrada no domínio e os detalhes de implementação, como banco de dados e interfaces do usuário, mantidos externamente a essa camada central. Isso permite que a aplicação seja resiliente a mudanças, seja em tecnologias ou em regras de negócio.

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2024.png" alt="Untitled">
</div>


A seguir, detalhamos os aspectos mais significativos da nossa API, com foco em Autenticação, Gestão de Erros, Segurança, Logging e Versionamente.

### 8.2 Autenticação

A autenticação na nossa API é feita utilizando o número pessoal do empregado como identificador. O endpoint `/api/v1/auth/authenticate` espera um `POST` com um corpo JSON contendo o `N_pessoal`. Apenas empregados com cargos de 'PRESIDENTE EXECUTIVO' ou 'VICE-PRESIDENTE' receberão um token JWT que permite acesso a informações restritas.

**Exemplo de solicitação de autenticação:**

```json
POST /api/v1/auth/authenticate
Content-Type: application/json

{
  "N_pessoal": 123456
}
```

**Respostas possíveis:**

- **200 OK**: Autenticação bem-sucedida, incluindo um token JWT no corpo da resposta.
- **400 Bad Request**: Número pessoal não encontrado ou inválido.
- **401 Unauthorized**: Empregado não tem permissão (não é presidente ou vice-presidente).

**Exemplo de resposta para usuário autorizado:**

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6..."
}

```

**Exemplo de resposta para usuário não autorizado:**

```json
{
  "message": "Acesso restrito ao presidente e vice-presidente."
}

```

### 8.3 Gestão de Erros

A API possui um mecanismo de gestão de erros que capta exceções não tratadas e retorna respostas padronizadas. O `ThrowController` é configurado para capturar e formatar essas exceções.

**Exemplos de mensagens de erro:**

- **Erro de servidor interno (500 Internal Server Error)**: "Ocorreu um erro ao obter empregados."
- **Recurso não encontrado (404 Not Found)**: "Nenhum empregado encontrado."

Em ambiente de desenvolvimento, a API fornece detalhes do stack trace para facilitar a depuração, enquanto em produção, esses detalhes são omitidos para segurança.

# 9. Dashboard Integrado com o Backend (V2)

# Dashboard - Versão 2

## Introdução

Neste artefato, apresentamos a segunda versão do Dashboard, que foi integrado com a API de Integração. O Dashboard foi desenvolvido seguindo os princípios de DRY (Don't Repeat Yourself), garantindo eficiência, coesão e reutilização de componentes. Abaixo detalhamos os principais aspectos desta versão.

## Interface do Dashboard Atualizada

A interface do Dashboard - Versão 2 foi atualizada para refletir os novos dados e funcionalidades da API V2. Esta nova interface inclui:

- **Gráficos Interativos:** Utilizamos a biblioteca NgApexcharts para criar gráficos interativos que exibem as métricas e informações relevantes. Por exemplo, o gráfico de barras exibe o número de colaboradores e atestados por planta.

```tsx
import { ChartComponent } from 'ng-apexcharts';

@Component({
  selector: 'app-plantas',
  templateUrl: './plantas.component.html',
  styleUrls: ['./plantas.component.css'],
})
export class PlantasComponent {
  @ViewChild('chart') chart!: ChartComponent;
  public chartOptions: Partial<ChartOptions>;

  constructor(private apiConnect: ApiconnectService) {
    this.chartOptions = {
      series: [],
      chart: {
        height: 350,
        width: 500,
        type: 'bar',
        zoom: {
          enabled: false,
        },
      },
      // outras configurações...
    };
  }

  ngOnInit() {
    this.apiConnect.getCid2023EmployeesCertificate().subscribe((response: HttpResponse<UnidadeResponse>) => {
      // processamento dos dados e atualização do gráfico...
    });
  }
}

```

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2025.png" alt="Untitled">
</div>


## Integração com API V2

O Dashboard - Versão 2 está integrado exclusivamente com a API de Integração V2 para obter os dados necessários. A integração foi realizada utilizando o serviço `ApiconnectService` para fazer solicitações HTTP à API V2 e recuperar os dados.

```tsx
@Injectable({
  providedIn: 'root'
})
export class ApiconnectService {
  BASE_URL = enviroment.apiUrl;
  constructor(private http: HttpClient) { }

  getCid2023EmployeesCertificate(): Observable<HttpResponse<UnidadeResponse>> {
    return this.http.get<UnidadeResponse>(`${this.BASE_URL}Cid/colaboradores-atestados-por-unidade`, { observe: 'response'});
  }
}

```

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2026.png" alt="Untitled">
</div>


## Princípio DRY

O princípio DRY foi aplicado de forma eficaz no código-fonte do Dashboard - Versão 2. As duplicações de código foram minimizadas e os componentes foram projetados para serem reutilizáveis. Por exemplo, na atualização dos dados do gráfico, evitamos repetir a lógica de processamento de dados.

```tsx
this.apiConnect.getCid2023EmployeesCertificate().subscribe((response: HttpResponse<UnidadeResponse>) => {
  // processamento dos dados e atualização do gráfico...
});

```

## Responsividade

O Dashboard - Versão 2 é responsivo e se adapta a diferentes tamanhos de tela e dispositivos. Isso foi alcançado utilizando técnicas de design responsivo e CSS flexível para garantir uma experiência consistente em todas as plataformas.

```css
/* Exemplo de estilo responsivo */
@media screen and (max-width: 600px) {
  .chart-container {
    width: 100%;
  }
}
```

## Performance

A performance do Dashboard - Versão 2 foi otimizada para garantir uma carga rápida e uma experiência fluida para o usuário. Isso inclui a minimização do tamanho dos recursos, a redução do número de solicitações de rede e o uso eficiente de cache sempre que possível.

```tsx
// Exemplo de cache de dados
@Injectable({
  providedIn: 'root'
})
export class CacheService {
  private cache = new Map<string, any>();

  constructor() { }

  get(key: string) {
    return this.cache.get(key);
  }

  set(key: string, value: any) {
    this.cache.set(key, value);
  }
}
```

## Conclusão

O Dashboard - Versão 2 representa um avanço significativo em relação à versão anterior, com melhorias na interface, integração com a API, aderência ao princípio DRY, responsividade e performance. Esperamos que esta versão atenda aos padrões de qualidade estabelecidos e proporcione uma experiência de usuário aprimorada.


# 11. Documentação Backend (Sprint 3)

### Documentação da API de Integração - Versão 2

Na construção do nosso backend temos a seguinte estrutura de pasta no Visual Studio Code 2022:

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2034.png" alt="Untitled">
</div>


1. **GitHub Actions**:

- Pasta que contém scripts e configurações relacionadas ao GitHub Actions para automação de fluxos de trabalho.

2. **Polo.Dashboard.WebApi**:

- Nome do projeto principal, onde estão organizados os arquivos relacionados a esse projeto específico.

3. **Connected Services**:

- Pasta que contém serviços conectados ao projeto, como APIs externas ou recursos de nuvem.

4. **Dependências**:

- Pasta que contêm referências e dependências externas do projeto, como bibliotecas ou pacotes de terceiros.

5. **Properties**:

- Essa pasta geralmente contém arquivos de configuração e definições relacionadas às propriedades do projeto.

6. **launchSettings.json**:

- Arquivo de configuração que contêm configurações de inicialização para o projeto, como perfis de depuração e ambientes.

7. **Application**:

- Pasta que contêm arquivos relacionados à lógica de aplicação do projeto.

8. **Services**:

- Pasta que contêm classes de serviços ou componentes que fornecem funcionalidades específicas para o projeto.

9. **SwaggerConfig**:

- Pasta que contém configurações relacionadas à documentação de API usando Swagger.

10. **Controllers**:

- Pasta que contêm os controladores do projeto, que são responsáveis por lidar com as solicitações HTTP e fornecer respostas apropriadas.

11. **v1** e **v2**:

- Versões dos controladores. Cada versão pode conter controladores diferentes ou variações de implementações.

12. **Domain**:

- Contêm arquivos relacionados ao domínio do projeto, como entidades de negócios e lógica de domínio.

13. **DTOs**:

- Abreviação para "Data Transfer Objects". Nessa pasta há classes que são usadas para transferir dados entre subsistemas do projeto.

14. **Model**:

- Contêm classes que representam modelos de dados usados no projeto.

15. **Infraestrutura**:

- Pasta que contém arquivos relacionados à infraestrutura do projeto, como configurações de banco de dados e integrações externas.

16. **Repositories**:

- Pasta que contêm classes que implementam a lógica de acesso a dados, como operações de leitura/gravação no banco de dados.

17. **ConnectionContext.cs**:

- Arquivo de código-fonte que contêm a implementação de um contexto de conexão usado para interagir com o banco de dados.

18. **appsettings.json** e **Dockerfile**:

- Arquivos de configuração para o projeto, contendo configurações de aplicativo e definições de contêiner Docker, respectivamente.

### Descrição

Esta é a segunda versão da API de integração, desenvolvida para incluir comunicação com o Dashboard - Versão 2. Com o objetivo de oferecer uma experiência mais avançada e funcionalidades adicionais, esta versão inclui novos serviços de feature para tratamento e manipulação de dados do frontend. Esses recursos foram projetados para permitir uma interação mais sofisticada com o dashboard, proporcionando aos usuários uma visão mais abrangente e insights mais profundos.

### Estrutura da API

A API consiste em endpoints cuidadosamente projetados para fornecer acesso aos recursos necessários tanto para a integração com o Dashboard - Versão 2 quanto para a manipulação avançada de dados do frontend. Cada endpoint é documentado detalhadamente, especificando sua descrição, parâmetros de consulta, autenticação necessária, formato de resposta e exemplos ilustrativos.

### Endpoints

### 1. Gptw

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2035.png" alt="Untitled">
</div>


- **Descrição:** Fornece acesso aos dados do GPTW.
- **Endpoints:**

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2036.png" alt="Untitled">
</div>


1. **GET /api/v1/gptw**: Retorna todos os dados do GPTW.
    - **Parâmetros de consulta:** Nenhum.
    - **Autenticação:** Requer autenticação com o papel "PresidenteFuncionarioPolicy".
    - **Formato de resposta:** JSON.
    - **Exemplo de resposta:**
        
        ```json
        [
            {
                "nniveis": "value",
                "uo_abrev": "value",
                "rrh": "value",
                "local": "value",
                "empr": "value",
                "gremp": "value",
                "grpempregados": "value",
                "sgemp": "value",
                "centrocst": "value",
                "unidorg": "value",
                "descr_uo": "value",
                "idadedoempregado": "value",
                "gn": "value",
                "data_adm": "value"
            }
        ]
        
        ```
        

1. **GET /api/v1/Gptw/gptw-quantidaderespostas**: Retorna os dados de quantidade de respostas na GPTW por unidade.

![Untitled](https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/1854ec91-0119-4f08-aec1-9b7580571a89.png)

1. **GET /api/v1/Gptw/engajamento**: Retorna a porcentagem sobre como está o engajamento da empresa na pesquisa GPTW.

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2037.png" alt="Untitled">
</div>


### 2. Empregado

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2038.png" alt="Untitled">
</div>

- **Descrição:** Fornece acesso aos dados dos empregados.
- **Endpoints:**
    
<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2039.png" alt="Untitled">
</div>
    

1. **GET /api/v1/Empregado**: Retorna todos os dados dos empregados.
    - **Parâmetros de consulta:** Nenhum.
    - **Autenticação:** Requer autenticação com os papéis "PRESIDENTE EXECUTIVO" ou "VICE-PRESIDENTE".
    - **Formato de resposta:** JSON.
    - **Exemplo de resposta:**
        
        ```json
        [
            {
                "n_pessoal": "value",
                "sg_emp": "value",
                "texto_rh": "value",
                "centro_cst": "value",
                "centro_custo": "value",
                "cargo": "value",
                "data_nascimento": "value"
            }
        ]
        
        ```
        

![Untitled](https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/73758aae-94cf-4829-b7ee-27934f0987c3.png)

1. **GET /api/v1/Empregado/seg**: Retorna os dados sobre os segmentos dos empregados e também a quantidade.

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2040.png" alt="Untitled">
</div>

### 3. Cid2023

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2041.png" alt="Untitled">
</div>


- **Descrição:** Fornece acesso aos dados do CID2023.
- **Endpoints:**

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2042.png" alt="Untitled">
</div>


1. **GET /api/v1/Cid**: Retorna todos os dados do CID2023.
    - **Descrição**: os dados do CID2023 são referentes aos dados dos colaboradores como: quantidade de atestados, diretoria e planta.
        - **Parâmetros de consulta:** Nenhum.
        - **Autenticação:** Requer autenticação.
        - **Formato de resposta:** JSON.
        - **Exemplo de resposta:**
            
            ```json
            [
                {
                    "mes": "value",
                    "atestados": "value",
                    "cid": "value",
                    "descricao": "value",
                    "dias": "value",
                    "diretoria": "value",
                    "unidade": "value"
                }
            ]
            ```

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2043.png" alt="Untitled">
</div>
            

1. **GET /api/v1/Cid/colaboradores-atestados-por-unidade**: Retorna os dados sobre a quantidade de atestados dos colaboradores por planta.
    - **Descrição:** Retorna um dicionário contendo informações sobre o número de colaboradores e atestados por unidade.
        - **Parâmetros de consulta:** Nenhum.
        - **Autenticação:** Requer autenticação.
        - **Formato de resposta:** JSON.
        - **Exemplo de resposta:**

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2044.png" alt="Untitled">
</div>


1. **GET /api/v1/Cid/certo-total**: Retorna os dados sobre o total de atestados por mês e por unidade.

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2045.png" alt="Untitled">
</div>

### 4. Stiba

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2048.png" alt="Untitled">
</div>

- **Descrição:** Notas da pesquisa Stiba
- **Endpoints:**
    
<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2049.png" alt="Untitled">
</div>
    
1. **GET /api/v1/Stiba**: 
    - Descrição: Todos os valores do Stiba.
        - **Parâmetros de consulta:** Nenhum.
        - **Autenticação:** Não requer autenticação.
        - **Formato de resposta:** JSON.
        - **Exemplo de resposta:**
            
            ```json
                {
                    "descricaoUO": "value",
                    "elegiveis": "value",
                    "particip": "value",
                    "notaStiba": "value"
                }
            ```
            

1. **GET /api/v1/media-nota**: 
    - Descrição: Média total das notas Stiba.
        - **Parâmetros de consulta:** Nenhum.
        - **Autenticação:** Não requer autenticação.
        - **Formato de resposta:** JSON.
        - **Exemplo de resposta:**
        
        ```json
        "value"
        ```
        
2. **GET /api/v1/media-local**: 
    - Descrição: Média das notas das plantas.
        - **Parâmetros de consulta:** Nenhum.
        - **Autenticação:** Não requer autenticação.
        - **Formato de resposta:** JSON.
        - **Exemplo de resposta:**
        
        ```json
            {
                "sigla_UO": "value",
                "average_nota_stiba": "value"
            }
        ```
        
3. **GET /api/v1/engajamento-local**: 
    - Descrição: Engajamento dos colaboradores à pesquisa Stiba entre as plantas.
        - **Parâmetros de consulta:** Nenhum.
        - **Autenticação:**  Não requer autenticação.
        - **Formato de resposta:** JSON.
        - **Exemplo de resposta:**
        
        ```json
            {
                "sigla_UO": "value",
                "average_engajamento": "value",
                "std_engajamento": "value"
            }
        ```
        
4. **GET /api/v1/engajamento-total**: 
    - Descrição: Engajamento total dos colaboradores à pesquisa Stiba.
        - **Parâmetros de consulta:** Nenhum.
        - **Autenticação:** Não requer autenticação.
        - **Formato de resposta:** JSON.
        - **Exemplo de resposta:**
        
        ```json
            {
                "avg": "value"
            }
        ```
        

### 5. Join

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2050.png" alt="Untitled">
</div>

- **Descrição:** Retorna os dados sobre mês, departamento, total de sessões, cargo, gênero, quantidade de atestados e a descrição detalhada por colaborador.
- **Endpoints:**

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2051.png" alt="Untitled">
</div>

1. **GET /api/v1/Join/join-data**:

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2052.png" alt="Untitled">
</div>

### 3.1 Testes

### 3.1.1 Pasta de testes Cid 2023

### 3.1.1.1 Introdução

A pasta possui um conjunto de testes que garantem a qualidade, confiabilidade e integridade do código. Esses testes abrangem desde a autenticação de usuários até o funcionamento correto do controlador e do repositório. 

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2046.png" alt="Untitled">
</div>


### 3.1.1.2 Estrutura de arquivos

- **`AuthenticateAsyncV2.cs`**: Este arquivo contém testes para a autenticação de usuários na API. Ele verifica se a autenticação funciona corretamente em diferentes cenários, como quando o usuário não é encontrado no banco de dados.

```csharp
using Xunit;
using Moq;
using Polo.Dashboard.WebApi.Controllers.v2;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Polo.Dashboard.WebApi.Tests
{
    public class AuthControllerTests
    {
        [Fact(DisplayName = "AuthenticateAsync returns BadRequest when user is not found")]
        public async Task AuthenticateAsync_ReturnsBadRequest_WhenUserNotFound()
        {
            // Arrange
            var mockEmpregadosRepository = new Mock<IEmpregadosRepository>();
            mockEmpregadosRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<EmpregadoDTO>());

            var mockRoleService = new Mock<IRoleService>();

            var controller = new AuthController(mockEmpregadosRepository.Object, mockRoleService.Object);

            // Act
            var result = await controller.AuthenticateAsync(new AuthenticateRequestDTO { N_pessoal = 999 });
            var badRequestResult = result as BadRequestObjectResult;

            // Assert
            Assert.NotNull(badRequestResult);
            Assert.Equal(400, badRequestResult?.StatusCode);
        }

        // ... outros testes ...
    }
}

```

- **`Cid2023ControllerTests.cs`**: Aqui estão os testes para o **`Cid2023Controller`**, verificando se ele lida corretamente com diferentes cenários de requisição. Os testes aqui garantem que o controlador responda corretamente a diferentes situações, como quando nenhum CID2023 é encontrado ou quando ocorre um erro ao recuperar os dados.

```csharp
using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using Polo.Dashboard.WebApi.Controllers.v1;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Tests.Cid2023
{
    public class Cid2023ControllerTests
    {
        [Fact(DisplayName = "Given empty Cid2023 list, should return NotFound")]
        public async Task GetAsync_ShouldReturnNotFound_WhenNoCid2023Found()
        {
            // Arrange
            var mockRepository = new Mock<ICid2023Repository>();
            mockRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<Cid2023DTO>());

            var mockLogger = new Mock<ILogger<Cid2023Controller>>();
            var mockMapper = new Mock<IMapper>();

            var controller = new Cid2023Controller(mockRepository.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Nenhum cid2023 encontrado.", notFoundResult.Value);
        }

        [Fact(DisplayName = "Given an error in retrieving Cid2023, should return StatusCode 500")]
        public async Task GetAsync_ShouldReturnStatusCode500_WhenErrorOccurs()
        {
            // Arrange
            var mockRepository = new Mock<ICid2023Repository>();
            mockRepository.Setup(repo => repo.GetAsync()).ThrowsAsync(new Exception("Test exception"));

            var mockLogger = new Mock<ILogger<Cid2023Controller>>();
            var mockMapper = new Mock<IMapper>();

            var controller = new Cid2023Controller(mockRepository.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
```

- **`Cid2023RepositoryMock.cs`**: Fornece um mock do repositório do CID2023 para uso nos testes. Ele simula o comportamento do repositório real, permitindo que os testes sejam executados de forma isolada e previsível. Este arquivo é essencial para criar um ambiente controlado nos testes, garantindo que as interações com o repositório sejam consistentes e previsíveis, independentemente do estado atual dos dados.

```csharp
using Moq;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Cid2023RepositoryMock
{
    public static Mock<ICid2023Repository> GetCid202
```

- **`Cid2023RepositoryTests.cs`**: Testes para garantir que o repositório do CID2023 funcione conforme o esperado em diferentes situações. Isso inclui testes para verificar se os dados são retornados corretamente e se o repositório lida adequadamente com erros.

```csharp
**using Xunit;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Tests.Cid2023
{
    public class Cid2023RepositoryTests
    {
        [Fact(DisplayName = "Given valid Cid2023 list, should return it and greater than zero")]
        public async Task GetAsync_ShouldReturnListOfCid2023()
        {
            // Arrange
            var mockRepository = Cid2023RepositoryMock.GetCid2023Repository();
            var cid2023Repository = mockRepository.Object;

            // Act
            var result = await cid2023Repository.GetAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Cid2023DTO>>(result);
            Assert.True(result.Count > 0);
        }
    }
}**
```

### 3.1.2 Pasta de Testes Empregados

### 3.1.2.1 Introdução

Os arquivos abaixo fazem parte de um conjunto de testes relacionados à gestão dos dados dos colaboradores. Partindo da autenticação de usuários até a interação com o repositório de empregados e o funcionamento do controlador de empregados. 

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2047.png" alt="Untitled">
</div>

### 3.1.1.2 Estrutura de arquivos

- **`AuthenticateAsync.cs`**: Este arquivo contém testes para a autenticação de usuários na API. Ele verifica se a autenticação funciona corretamente em diferentes cenários, como quando o usuário não é encontrado no banco de dados. O teste específico verifica se um BadRequest é retornado quando o usuário não é encontrado.

```csharp
using Microsoft.AspNetCore.Mvc;
using Moq;
using Polo.Dashboard.WebApi.Controllers.v1;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Polo.Dashboard.WebApi.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Polo.Dashboard.WebApi.Controllers.v2;

namespace Polo.Dashboard.WebApi.Tests.Empregados
{
    public class AuthControllerTests
    {
        [Fact(DisplayName = "Given an employee number and role, return UserNotFound ")]
        public async Task AuthenticateAsync_ReturnsBadRequest_WhenUserNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IEmpregadosRepository>();
            var mockRoleService = new Mock<IRoleService>();

            mockRepo.Setup(repo => repo.GetAsync()).ReturnsAsync(new List<EmpregadoDTO>());

            var controller = new AuthController(mockRepo.Object, mockRoleService.Object);

            // Act
            var result = await controller.AuthenticateAsync(new AuthenticateRequestDTO { N_pessoal = 1 });

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

    }
}

```

- **`EmpregadosRepositoryMock.cs`**: Este arquivo fornece um mock do repositório de empregados para uso nos testes. Ele simula o comportamento do repositório real, permitindo que os testes sejam executados de forma isolada e previsível. Os dados de empregados são criados para simular interações com o repositório.

```csharp
using Moq;
using Polo.Dashboard.WebApi.Domain.DTOs;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;

public class EmpregadosRepositoryMock
{
    public static Mock<IEmpregadosRepository> GetEmpregadosRepository()
    {
        var mockRepository = new Mock<IEmpregadosRepository>();
        var empregados = new List<EmpregadoDTO>
        {
            new EmpregadoDTO
            {
                n_pessoal = 0,
                sg_emp = "EX",
                texto_rh = "EX",
                centro_cst = 1222,
                centro_custo = "EX",
                cargo = "EX",
                data_nascimento = "01/01/2001"
            },
            new EmpregadoDTO
            {
                n_pessoal = 1,
                sg_emp = "EX2",
                texto_rh = "EX2",
                centro_cst = 33333,
                centro_custo = "EX2",
                cargo = "EX2",
                data_nascimento = "01/01/2002"
            }
        };

        mockRepository.Setup(r => r.GetAsync()).ReturnsAsync(empregados);

        return mockRepository;
    }
}
```

- **`EmpregadosRepositoryTests.cs`**: Aqui estão os testes para o repositório de empregados, garantindo que ele funcione conforme o esperado em diferentes situações. Isso inclui testes para verificar se os dados são retornados corretamente e se o repositório lida adequadamente com erros.

```csharp
**using Polo.Dashboard.WebApi.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polo.Dashboard.WebApi.Tests.Empregados
{
    public class EmpregadosRepositoryTests
    {
        [Fact(DisplayName = "Given valid Employee list, should return it and greater than zero" )]
        public async Task GetAsync_ShouldReturnListOfEmpregados()
        {
            // Arrange
            var mockRepository = EmpregadosRepositoryMock.GetEmpregadosRepository();
            var empregadosRepository = mockRepository.Object;

            // Act
            var result = await empregadosRepository.GetAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<EmpregadoDTO>>(result);
            Assert.True(result.Count > 0);
        }
    }
}**

```

- **`EmpregadosTests.cs`**: Este arquivo contém testes para a classe de modelo de empregados. Ele verifica se a criação de um objeto empregado atribui corretamente os valores às propriedades

```csharp
using Xunit;
using Polo.Dashboard.WebApi.Domain.Model;

namespace Polo.Dashboard.WebApi.Tests.Empregados
{
    public class EmpregadosTests
    {
        [Fact(DisplayName = "Given an employee atributes, then should create an employee")]
        public void Empregado_Constructor_ShouldSetProperties()
        {
            // Arrange
            int n_pessoal = 12345;
            string sg_emp = "ABC";
            string texto_rh = "Texto RH";
            int centro_cst = 6789;
            string centro_custo = "Centro Custo";
            string cargo = "Cargo";
            string data_nascimento = "01/01/1980";

            // Act
            var empregado = new Domain.Model.Empregados(n_pessoal, sg_emp, texto_rh, centro_cst, centro_custo, cargo, data_nascimento);

            // Assert
            Assert.Equal(n_pessoal, empregado.n_pessoal);
            Assert.Equal(sg_emp, empregado.sg_emp);
            Assert.Equal(texto_rh, empregado.texto_rh);
            Assert.Equal(centro_cst, empregado.centro_cst);
            Assert.Equal(centro_custo, empregado.centro_custo);
            Assert.Equal(cargo, empregado.cargo);
            Assert.Equal(data_nascimento, empregado.data_nascimento);
        }
    }
}
```

- **`GetAsync.cs`**: Este arquivo contém testes para o controlador de empregados, especificamente para o método GetAsync. Ele verifica se o método retorna um resultado Ok com a lista de empregados quando chamado. As dependências são simuladas usando mocks.

```csharp
**using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Polo.Dashboard.WebApi.Controllers.v1;
using Polo.Dashboard.WebApi.Domain.Model.Interfaces;
using Polo.Dashboard.WebApi.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace Polo.Dashboard.WebApi.Tests.Empregados
{
    public class EmpregadoControllerTests
    {
        [Fact]
        public async Task GetAsync_ReturnsOkResult_WithEmpregados()
        {
            // Arrange
            var mockRepository = new Mock<IEmpregadosRepository>();
            var empregadosDTOList = new List<EmpregadoDTO>
            {
                new EmpregadoDTO
                {
                    n_pessoal = 1,
                    sg_emp = "XXX",
                    texto_rh = "XXX",
                    centro_cst = 1,
                    centro_custo = "XXX",
                    cargo = "XXX",
                    data_nascimento = "XXX"
                },
                new EmpregadoDTO
                {
                    n_pessoal = 2,
                    sg_emp = "YYY",
                    texto_rh = "YYY",
                    centro_cst = 2,
                    centro_custo = "YYY",
                    cargo = "YYY",
                    data_nascimento = "YYY"
                }
            };

            mockRepository.Setup(repo => repo.GetAsync()).ReturnsAsync(empregadosDTOList);

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(mapper => mapper.Map<List<EmpregadoDTO>>(It.IsAny<List<EmpregadoDTO>>()))
                .Returns((List<EmpregadoDTO> source) => source);

            var mockLogger = new Mock<ILogger<EmpregadoController>>();
            var controller = new EmpregadoController(mockRepository.Object, mockLogger.Object, mockMapper.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<EmpregadoDTO>>(actionResult.Value);
            Assert.NotEmpty(returnValue);
        }
    }
}**
```

### Controllers

### 1. GPTWController.cs

O **`GptwController`** é um controlador ASP.NET Core responsável por lidar com as requisições relacionadas aos dados GPTW (Great Place to Work) através de uma API REST. Este controlador expõe endpoints para obter informações sobre GPTW, quantidades de respostas locais e engajamento.

**Atributos do Controlador**

- **`[Route("api/v{version:apiVersion}/[controller]")]`**: define o padrão de rota para o controlador, incluindo uma versão da API.
- **`[ApiController]`**: indica que o controlador é uma API.
- **`[ApiVersion("1.0")]`**: especifica a versão da API que este controlador suporta.

**Métodos do Controlador**

### 1. **`GetAsync()`**

- **Tipo**: **`HttpGet`**
- **Descrição**: Obtém uma lista de GPTW.
- **Retorno**: **`Task<IActionResult>`**
- **Endpoints**: **`/api/v1.0/gptw`**

**Comportamento:**

- O método recupera os dados do repositório.
- Os dados são mapeados para DTOs usando o AutoMapper.
- Se nenhum GPTW for encontrado, retorna um status 404 (Not Found).
- Se ocorrer um erro, retorna um status 500 (Internal Server Error).

### 2. **`GetLocalCountsAsync()`**

- **Tipo**: **`HttpGet`**
- **Descrição**: Obtém a contagem de GPTW por local.
- **Retorno**: **`Task<IActionResult>`**
- **Endpoints**: **`/api/v1.0/gptw/gptw-quantidaderespostas`**

**Comportamento:**

- O método recupera a contagem de GPTW por local do repositório.
- Se nenhuma contagem for encontrada, retorna um status 404 (Not Found).
- Se ocorrer um erro, retorna um status 500 (Internal Server Error).

### 3. **`GetEngajamentoAsync()`**

- **Tipo**: **`HttpGet`**
- **Descrição**: Obtém os dados de engajamento relacionados ao GPTW.
- **Retorno**: **`Task<IActionResult>`**
- **Endpoints**: **`/api/v1.0/gptw/engajamento`**

**Comportamento:**

- O método recupera os dados de engajamento do repositório.
- Se nenhum dado de engajamento for encontrado, retorna um status 404 (Not Found).
- Se ocorrer um erro, retorna um status 500 (Internal Server Error).

**Logging**

O controlador utiliza o **`ILogger`** para registrar informações, avisos e erros durante a execução dos métodos. Isso ajuda a monitorar e diagnosticar problemas na aplicação.

**Tratamento de Erros**

Todos os métodos do controlador têm um bloco **`try-catch`** para capturar exceções. Em caso de erro, é retornado um status 500 (Internal Server Error) junto com uma mensagem de erro genérica.

### 2. Cid2023Controller.cs

O **`Cid2023Controller`** é um controlador ASP.NET Core responsável por lidar com as requisições relacionadas aos dados CID 2023 através de uma API REST. Utiliza a versão 1.0 da API e expõe endpoints para obter informações sobre CID 2023, colaboradores e atestados por unidade, e o total de atestados.

**Atributos do Controlador**

- **`[Route("api/v{version:apiVersion}/[controller]")]`**: Define o padrão de rota para o controlador, incluindo uma versão da API.
- **`[ApiController]`**: Indica que o controlador é uma API.

**Métodos do Controlador**

### 1. **`GetAsync()`**

- **Tipo**: **`HttpGet`**
- **Descrição**: Obtém uma lista de CID 2023.
- **Retorno**: **`Task<IActionResult>`**
- **Endpoints**: **`/api/v1.0/cid2023`**

**Comportamento:**

- O método recupera os dados do repositório.
- Os dados são mapeados para DTOs usando o AutoMapper.
- Se nenhum CID 2023 for encontrado, retorna um status 404 (Not Found).
- Se ocorrer um erro, retorna um status 500 (Internal Server Error).

### 2. **`GetColaboradoresAtestadosPorUnidadeAsync()`**

- **Tipo**: **`HttpGet`**
- **Descrição**: Obtém informações de colaboradores e atestados por unidade.
- **Retorno**: **`Task<IActionResult>`**
- **Endpoints**: **`/api/v1.0/cid2023/colaboradores-atestados-por-unidade`**

**Comportamento:**

- O método recupera as informações de colaboradores e atestados por unidade do repositório.
- Se nenhuma informação for encontrada, retorna um status 404 (Not Found).
- Se ocorrer um erro, retorna um status 500 (Internal Server Error).

### 3. **`GetCertoTotalAsync()`**

- **Tipo**: **`HttpGet`**
- **Descrição**: Obtém o total de atestados.
- **Retorno**: **`Task<IActionResult>`**
- **Endpoints**: **`/api/v1.0/cid2023/certo-total`**

**Comportamento:**

- O método recupera o total de atestados do repositório.
- Se nenhum total de atestados for encontrado, retorna um status 404 (Not Found).
- Se ocorrer um erro, retorna um status 500 (Internal Server Error).

**Logging**

O controlador utiliza o **`ILogger`** para registrar informações, avisos e erros durante a execução dos métodos. Isso ajuda a monitorar e diagnosticar problemas na aplicação.

**Tratamento de Erros**

Todos os métodos do controlador têm um bloco **`try-catch`** para capturar exceções. Em caso de erro, é retornado um status 500 (Internal Server Error) junto com uma mensagem de erro genérica.

### 3. EmpregadoController.cs

O **`EmpregadoController`** é um controlador ASP.NET Core responsável por lidar com as requisições relacionadas aos dados de empregados através de uma API REST. Expõe endpoints para obter informações sobre empregados e dados de segmentação.

**Métodos do Controlador**

### 1. **`GetAsync()`**

- **Tipo**: **`HttpGet`**
- **Descrição**: Obtém uma lista de empregados.
- **Retorno**: **`Task<IActionResult>`**
- **Endpoints**: **`/api/v1.0/empregado`**

**Comportamento:**

- O método recupera os dados do repositório.
- Os dados são mapeados para DTOs usando o AutoMapper.
- Se nenhum empregado for encontrado, retorna um status 404 (Not Found).
- Se ocorrer um erro, retorna um status 500 (Internal Server Error).

### 2. **`GetSegAsync()`**

- **Tipo**: **`HttpGet`**
- **Descrição**: Obtém dados de segmentação.
- **Retorno**: **`Task<IActionResult>`**
- **Endpoints**: **`/api/v1.0/empregado/seg`**

**Comportamento:**

- O método recupera os dados de segmentação do repositório.
- Se nenhum dado de segmentação for encontrado, retorna um status 404 (Not Found).
- Se ocorrer um erro, retorna um status 500 (Internal Server Error).

**Logging**

O controlador utiliza o **`ILogger`** para registrar informações, avisos e erros durante a execução dos métodos. Isso ajuda a monitorar e diagnosticar problemas na aplicação.

**Tratamento de Erros**

Todos os métodos do controlador têm um bloco **`try-catch`** para capturar exceções. Em caso de erro, é retornado um status 500 (Internal Server Error) junto com uma mensagem de erro genérica.

### 4. JoinController.cs

É um controlador ASP.NET Core responsável por lidar com as requisições relacionadas aos dados de "join" através de uma API REST. Este controlado expõe um endpoint para obter informações de mês, departamento, total de sessões, cargo, gênero, descrição detalhada e quantidade de atestados por colaborador.

**Métodos do Controlador**

### 1. **`GetJoinData()`**

- **Tipo**: **`HttpGet`**
- **Descrição**: Obtém dados de "join": mês, departamento, total de sessões, cargo, gênero, descrição detalhada e quantidade de atestados por colaborador.
- **Retorno**: **`Task<IActionResult>`**
- **Endpoints**: **`/api/v1.0/join/join-data`**

**Comportamento:**

- O método recupera os dados do repositório.
- Os dados são mapeados para DTOs usando o AutoMapper.
- Se nenhum dado de "join" for encontrado, retorna um status 404 (Not Found).
- Se ocorrer um erro, retorna um status 500 (Internal Server Error).

**Logging**

O controlador utiliza o **`ILogger`** para registrar informações, avisos e erros durante a execução dos métodos. Isso ajuda a monitorar e diagnosticar problemas na aplicação.

**Tratamento de Erros**

O método do controlador tem um bloco **`try-catch`** para capturar exceções. Em caso de erro, é retornado um status 500 (Internal Server Error) junto com uma mensagem de erro genérica.

### 5. StibaController.cs

O **`StibaController`** é um controlador da API responsável por lidar com operações relacionadas aos dados do Stiba. Ele permite a obtenção de dados do Stiba, cálculo da média de notas, obtenção da média por local e obtenção do engajamento por local e total.

- Este controlador utiliza a injeção de dependência para obter o repositório do Stiba, o logger e o mapeador.
- Os métodos retornam os resultados em formato JSON.
- Caso ocorram erros durante as operações, o controlador retorna códigos de status e mensagens de erro.

Possui os seguintes métodos:

1. **GET /api/v{version}/stiba**
    - Descrição: Obtém todos os dados do Stiba.
    - Resposta:
        - 200 OK: Retorna a lista de dados do Stiba.
        - 404 Not Found: Caso não haja dados do Stiba disponíveis.
2. **GET /api/v{version}/stiba/media-nota**
    - Descrição: Calcula a média das notas do Stiba.
    - Resposta:
        - 200 OK: Retorna a média arredondada das notas.
        - 500 Internal Server Error: Em caso de erro ao calcular a média.
3. **GET /api/v{version}/stiba/media-local**
    - Descrição: Obtém a média por local no Stiba.
    - Resposta:
        - 200 OK: Retorna a média por local.
        - 500 Internal Server Error: Em caso de erro ao obter a média por local.
4. **GET /api/v{version}/stiba/engajamento-local**
    - Descrição: Obtém o engajamento por local no Stiba.
    - Resposta:
        - 200 OK: Retorna o engajamento por local.
        - 500 Internal Server Error: Em caso de erro ao obter o engajamento por local.
5. **GET /api/v{version}/stiba/engajamento-total**
    - Descrição: Obtém o engajamento total no Stiba.
    - Resposta:
        - 200 OK: Retorna o engajamento total.
        - 500 Internal Server Error: Em caso de erro ao obter o engajamento total.

### Integração com Dashboard V2

 A API de integração se comunica com o Dashboard - Versão 2 por meio dos endpoints fornecidos acima. Os dados são transmitidos em formato JSON para garantir uma comunicação eficiente entre os sistemas.

### Serviços de Feature

Foram adicionados novos serviços de feature para tratamento e manipulação avançada de dados pelo frontend. Esses serviços permitem uma interação mais sofisticada com o dashboard e incluem:

- **Recursos avançados de manipulação de dados:** Permitindo a realização de operações complexas nos dados fornecidos pela API.
- **Recursos de consulta avançada:** Permitindo a recuperação de dados com base em critérios específicos definidos pelo usuário.

### Estrutura de Dados Atualizada

As estruturas de dados (JSON) utilizadas para a entrada e saída da API foram atualizadas para incluir os novos recursos adicionados. Isso garante a consistência e a compatibilidade dos dados transmitidos entre a API e o Dashboard - Versão 2.

### Autenticação e Autorização

A API utiliza o esquema de autenticação JWT (JSON Web Token) para autenticar os usuários e autorizar o acesso aos recursos. Além disso, foram adicionadas políticas de autorização para garantir que apenas usuários autorizados possam acessar determinados endpoints.

### Gestão de Erros

A API continua a fornecer mensagens de erro informativas e orientações claras para solucionar problemas. Foram adicionados novos cenários relacionados aos feature services para garantir um tratamento adequado de erros e status codes. 

A API é capaz de identificar e comunicar erros específicos que podem ocorrer durante a execução. Cada tipo de erro é claramente identificado e acompanhado por uma mensagem adequada. Além disso, são utilizados os códigos de status HTTP apropriados para indicar o resultado da requisição, proporcionando uma compreensão rápida do estado da operação. Além do código de status HTTP, os corpos de resposta incluem detalhes adicionais para fornecer informações mais específicas sobre o erro ocorrido, ajudando na identificação e resolução do problema.

### Monitoramento e Logging

Propomos mecanismos de monitoramento atualizados, considerando as novas funcionalidades adicionadas. Isso inclui métricas de desempenho e registros (logs) para facilitar a depuração e o diagnóstico de problemas.

### Padrão de Qualidade

 A API de Integração - Versão 2 segue as diretrizes estabelecidas, incluindo as novas funcionalidades adicionadas. Ela mantém:

- **Interoperabilidade:** Capacidade de interoperar eficazmente com sistemas e serviços distintos, incluindo o Dashboard - Versão 2.
- **Documentação clara e completa:** Documentação precisa, atualizada e facilmente compreensível, incluindo os novos serviços.
- **Segurança:** Rigoroso controle de segurança, especialmente

# 12. Deploy

O Git Actions permite automatizar todo o processo de integração contínua e entrega contínua (CI/CD), desde a compilação do código-fonte até a implantação nos ambientes de produção. Isso traz benefícios, como a redução de erros humanos, a agilidade na entrega de novas funcionalidades e a garantia de que o software esteja sempre em um estado funcional e testado. 

O uso do Render para realizar o deploy adiciona uma camada adicional de eficiência e praticidade ao processo. Render é uma plataforma de hospedagem que simplifica a implantação de aplicativos na nuvem, oferecendo integração direta com Git Actions e outras ferramentas de CI/CD.

Portanto, o deploy foi realizado utilizando do Git Actions para realizar o deploy não apenas simplifica o processo, mas também aumenta a eficiência e a confiabilidade da entrega de software.

A seguir será detalhado o processo de deploy realizado para o projeto, fornecendo informações sobre as etapas do processo de deploy, como o sistema foi implantado e configurado para execução.

### 12.1 Deploy Backend

O deploy do backend envolve uma série de etapas que garantem o funcionamento  do sistema. Inicia-se com o processo de construção (build) que compila e prepara o código-fonte para implantação. Durante essa fase, são realizadas verificações de integridade e configurações específicas conforme necessário.

Após a etapa de construção, o próximo passo é a implantação propriamente dita. Aqui, o sistema é configurado para ser executado no ambiente de produção, o que pode envolver a instalação de dependências, configuração de variáveis de ambiente, ajustes de segurança e outros procedimentos essenciais.

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2053.png" alt="Untitled">
</div>

Figura: Diagrama das etapas realizadas para o deploy da API, com o status como Sucesso.

Como é possível verificar na figura do diagrama de deploy da API do Backend, processo de construção foi bem-sucedido, levando 2 minuto e 35 segundos, que envolveu verificar a versão do runner, detalhes do sistema operacional, e outras configurações essenciais para o ambiente de execução.

Durante o processamento do deploy, foi realizado o checkout do repositório, garantindo que todas as informações necessárias fossem sincronizadas corretamente. 

Na etapa de restauração das dependências da API,  todas as bibliotecas necessárias foram verificadas, para que estivessem disponíveis para o processo de construção seguinte. Este último consistiu na compilação da API, utilizando o comando dotnet build com a configuração Release. Apesar de algumas advertências sobre a compatibilidade de pacotes com o framework alvo, a compilação foi concluída com sucesso.

Após o checkout, ocorreram tarefas pós-checkout para limpar quaisquer resíduos do processo anterior e configurar adequadamente as informações do Git. Finalmente, o job foi considerado completo, com a limpeza de processos órfãos para garantir um ambiente limpo para futuras execuções. Essas etapas são contribuíram para um deploy bem-sucedido da API.

É possível ter uma visualização disso no seguinte link:

https://two024-t0004-si09-g05-api-dgnb.onrender.com/api/v1/Cid/certo-total

Esse é um exemplo de deploy do endpoint Cid2023

A visualização é a seguinte:

![Untitled](https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Deploy_exemplo_cid.png)


### 12.2 Deploy Frontend

Nesta etapa do processo de deploy, o objetivo é a implantação do frontend do sistema, que inclui o dashboard desenvolvido para visualização e interação com os dados, mas esse processo enfrenta desafios significativos. 

Inicialmente, a compilação e empacotamento do código-fonte exigem garantir a integridade e otimização dos recursos. A configuração do ambiente de produção envolve ajustes para garantia do funcionamento correto do dashboard em diferentes cenários. E por fim, os testes de integração são utilizados para identificar e corrigir possíveis falhas antes e durante a implantação. A etapa de implantação em si requer cuidados na configuração do servidor e nas permissões, visando uma distribuição estável.

<div align="center">
  <img src="https://github.com/Inteli-College/2024-T0004-SI09-G05-API/blob/main/assets/Untitled%2054.png" alt="Untitled">
</div>

Figura: Diagrama das etapas realizadas para o deploy do Dashboard, com o status como Falha na etapa de Build.

Como foi possível verificar na figura do diagrama do deploy do Dashboard, o deploy não foi realizado, por um erro no deploy do frontend, que ocorreu durante a etapa de Build, onde o sistema tentou compilar o código do Angular para produção. O problema específico foi que não foi possível encontrar o pacote do Angular DevKit responsável pelo processo de build para aplicativos. 

**Error: Could not find the '@angular-devkit/build-angular:application' builder's node package.**

- Esta é a parte crítica do erro. Ele indica que houve uma falha na execução do comando **`ng build`**, pois o Angular não conseguiu encontrar o pacote do Angular DevKit necessário para o processo de build.
