<h1>TaskManager - Fullstack (.NET & React)</h1>
<p>CRUD desenvolvido para o desafio técnico de transição Java/C#, focado em <strong>Clean Architecture</strong> e desacoplamento via DTOs.</p>

<h1>🏗️ Arquitetura em Camadas</h1>
<ul>
    <li><strong>Controllers:</strong> <code>TaskController</code> e <code>UserController</code> gerenciam os endpoints REST e retornos HTTP (200, 201, 204, 404).</li>
    <li><strong>Services:</strong> <code>TaskService</code> e <code>UserService</code> isolam a regra de negócio e utilizam o <strong>AutoMapper</strong> para transformar Entidades em DTOs.</li>
    <li><strong>Repositories:</strong> Camada de persistência que utiliza <strong>Entity Framework Core</strong> para comunicação com o MySQL.</li>
    <li><strong>DTOs:</strong> Objetos de transferência (Create, Update, Response) que protegem a integridade dos modelos internos.</li>
</ul>

<h1>🔄 Fluxo de Dados (Exemplo: Task)</h1>
<p>O caminho de uma requisição segue este ciclo de vida:</p>
<ol>
    <li><strong>Client (React/Axios):</strong> Envia um <code>JSON</code> (ex: <code>TaskCreateDTO</code>) para o endpoint <code>POST /api/task</code>.</li>
    <li><strong>Controller:</strong> Recebe o DTO e chama o método <code>CreateAsync</code> do Service.</li>
    <li><strong>Service:</strong> 
        <ul>
            <li>Mapeia o DTO para a Entidade <code>TaskItem</code> via AutoMapper.</li>
            <li>Chama o Repository para adicionar o objeto ao contexto.</li>
            <li>Persiste os dados com <code>SaveAsync()</code>.</li>
        </ul>
    </li>
    <li><strong>Repository:</strong> Executa o comando <code>INSERT</code> no MySQL via EF Core.</li>
    <li><strong>Resposta:</strong> O Service converte a Entidade salva em um <code>TaskResponseDTO</code> e o Controller retorna <code>201 Created</code>.</li>
</ol>

<h1>🛠️ Stack Técnica</h1>
<p>C# (.NET 6), React, MySQL (Docker), Entity Framework Core, AutoMapper e Custom Exceptions.</p>

<h1>🚀 Execução</h1>

<h3>1. Infraestrutura (Docker)</h3>
<pre>docker-compose up -d</pre>

<h3>2. Backend (API)</h3>
<pre>
dotnet restore
dotnet ef database update
dotnet run
</pre>

<h3>3. Frontend (Web)</h3>
<pre>
npm install
npm start
</pre>

<h1>🛡️ Gestão de Erros</h1>
<p>A aplicação utiliza exceções customizadas (<code>NotFoundException</code>, <code>BadRequestException</code>) capturadas por um middleware global, garantindo respostas padronizadas:</p>
<pre>
{
  "statusCode": 404,
  "message": "Tarefa com ID 10 não encontrada."
}
</pre>