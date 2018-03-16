# eticadata ERP
## Repositório de exemlos de customização
1. Exemplo de __User Controls__

    O projeto `Eticadata.Cust.Desktop` disponibiliza exemplos de como adicionar um novo user control ao ERP.

    Este projeto depois de compilado criará uma dll que deverá ser colocado na pasta bin do ERP `[DRIVER]:\Program Files (x86)\eticadata software\ERP v17\Desktop\bin\`. Adicionalmente a .dll deverá ser adicionada aos assemblies, acedendo ao separador Admin \ Customizações \ Assemblies que se encontra no eticadata ERP.

    No ficheiro `init.cs`, que está dentro da pasta `Eticadata.Cust.Desktop`, poderá encontrar no contrutor da class a forma como o contexto aplicacional é disponibilizado na class.

    ```csharp
    [InjectionConstructor()]
    public Init([ServiceDependency()] WorkItem myWorkItem_,
    [ServiceDependency()] EtiAplicacao myEtiApp_,
    [ServiceDependency()] UIUtils myUiutils_)
    {
        myEtiApp = myEtiApp_;
        myUiutils = myUiutils_;
        myWorkItem = myWorkItem_;
    } 
    ```

    É também nesta class `init.cs` que são referenciadas as janelas customizadas. A função `public void OnShowIntegraDocs` tem um `[CommandHandler("cmdNewWindow")]`. Para disponibilizar a janela customizada na ribbon, acedendo ao separador Admin \ Edição da Ribbon poderá adicionar um novo item onde o comando terá que ser: *cmdNewWindow*.

    ```csharp
    [CommandHandler("cmdNewWindow")]
    public void OnShowIntegraDocs(object sender, EventArgs e)
    {
        //Nome do userControl (nova janela a colocar na ribbon)
        ShowWindow<NewWindow>(false, true);
    } 
    ```

3. Exemplo de __Webservices__

    O projeto `Eticadata.Cust.Webservices` disponibiliza exemplos de como adicionar um webservice customizado ao ERP.

    As dll's geradas após a compilação devem ser copiadas para a pasta `[DRIVER]:\eticadata Sites\ERP v17\Eticadata.Web\Bin\`.

    Adicionalmente é necessário incluir a assembly na lista de assemblies a ser carregadas pelo site. Será necessário editar o ficheiro `Web.Config`, e adicionar a seguinte linha, conforme exemplo.

    ```xml
    <configuration>
        <system.web>
            <compilation>
                <assemblies>
                    <add assembly="Eticadata.Cust.WebServices" />
    ```
    
    Após publicação do webservice no site do ERP, é possivel invocar os webservices customizados acedendo ao URL: `http:\\<ServerName>\erpv17\api\<ControllerName>\[actionName]\`, no caso do exemplo disponibilizado `http:\\<ServerName>\erpv17\api\CustUtilities\PrintReport\`.

    Alguns `controllers` podem ser protegidos através da anotação `[Authorize]`. Desta forma é possivel garantir que o Webservice apenas está disponivel após a existencia de uma sessão válida no eticadata ERP. (Saiba mais em https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles).

    Sempre que utilizar a anotação `[Authorize]`, os pedidos ao webservice terão que obrigatóriamente passar pela inicialização de uma sessão, através de pedidos a webservices que existem de base no ERP.

    __Autenticação de utilizador__
    
    O primeiro pedido a ser feito ao webservice é a autenticação de utilizador.

    ```
    POST    http://localhost/ERPV17/api/Shell/LoginUser/
            Content-Type: application/json; charset=UTF-8

    
    {   
        "login": "demo",
        "password": "DEMO",
        "idioma": "pt-pt",
        "server": "LOCALHOST\SQLEXPRESS",
        "sistema": "SISTEMA"
    }
    ```
    O webservice responde com um JSON, onde, entre outras informações existe um valor boleano que indica se os dados são válidos.

    Caso a resposta do webservice anterior seja válida, será necessário indicar qual a base de dados sobre a qual serão executadas as operações.

    ```
    POST    http://localhost/ERPV17/api/Shell/OpenCompany/
            Content-Type: application/json; charset=UTF-8


    {
        "reabertura":true,
        "mostrarJanelaIniSessao":false,
        "codEmpresa":"DEMO",
        "codExercicio":"2018",
        "codSeccao":"1"
    }
    ```

    É importante referir que a autenticação é baseada em cookies. Assim sendo é necessário ter em conta que todos os pedidos aos webservices deve conter no Header os cookies de sessão. É também importante ter em conta que em cada resposta dos webservice podem ser retornados novos cookies.

    Após a inovacações dos webservices necessários, customizados ou não, é imprescinvidel terminar a sessão. Esta operação é feita através da invocação de um webservice de Logout.

    Este webservice libertará o licenciamento utilizado durante as operações realizadas no ambito da sessão, e terminará a sessão.

    ```
    POST    http://localhost/ERPV17/api/Shell/LogoutUser/
            Content-Type: application/json; charset=UTF-8
    ```

    


