# Como rodar o projeto
Após clonar o repositório, executar as seguintes linhas de comando para preparar o banco de dados no SQL Server
```
cd Viceri_Controle_Usuarios
dotnet ef migrations add CreateInitial
dotnet ef database update
```
E então
```
dotnet run
```

# Considerações
Decidi usar a versão 6 do .NET e a Entity Framework, por ter a liberdade de começar o projeto do zero achei melhor optar pelas versões mais recentes. Porém, não seria um problema fazer o mesmo em outras versões mais antigas.

Com isso, a framework "esconde" muito do processo de persistência no banco, sendo assim não foi necessário escrever explicitamente as queries, mas, disponibilizei na pasta Database os scripts que seriam necessários para rodar cada chamada da API.

Em Controller/UsuarioController é onde se encontram todas as implementações das chamadas, no projeto não optei por usar routing pois a interface ficou definida como a documentação do Swagger.

Enquanto ao Swagger, não consegui alterar schemas de exemplo para retornos, acredito que a SwashBuckle não permite tal customização, por isso em alguns casos terá como exemplo "StatusCode: 0", pois não era possível alterar essa informação.

# Hashing
Optei por fazer o hashing da senha diretamente no código fonte, para evitar operações mais complexas no banco.
