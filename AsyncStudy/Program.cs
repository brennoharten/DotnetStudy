void LerArquivo()
{
    var texto = File.ReadAllText("arquivo.txt");
    Console.WriteLine(texto);
}

//Utilizando Threads
Thread thread = new Thread(() => LerArquivo());
thread.Start();

while (thread.IsAlive)
{
    Console.WriteLine("Lendo arquivo...");
    Thread.Sleep(1000);
}


LerArquivo();
Console.WriteLine("Fim do programa");

//-----------------------------------------------------

//Utilizando Tasks
var task = Task.Run(() => LerArquivo());
while (!task.IsCompleted)
{
    Console.WriteLine("Lendo arquivo...");
    Task.Delay(1000).Wait();
}


//-----------------------------------------------------
// armazenando valor em uma task
Task<string> taskComValor = Task.Run(() => LerArquivoComValor());
console.WriteLine(taskComValor.Result);

//-----------------------------------------------------
//tratando exceção em uma task
try
{
    Task taskComErro = Task.Run(() => LerArquivoComErro());
    taskComErro.Wait();
}
catch (AggregateException ex)  //AggregateException é a exceção lançada quando uma ou mais exceções ocorrem durante a execução de uma tarefa.
{
    Console.WriteLine("Ocorreu um erro: " + ex.InnerException.Message); //Aqui eu estou vendo a exceção interna, que é a exceção real que ocorreu dentro da tarefa.
}


//-----------------------------------------------------
//Utilizando async/await

/* Ao utilizar async/await, o código fica mais simples e fácil de ler,
pois ele permite escrever código assíncrono de forma semelhante ao código síncrono.
O método LerArquivoAsync é marcado como async, o que indica que ele pode conter operações assíncronas.
O await é usado para esperar a conclusão da operação de leitura do arquivo, sem bloquear a thread principal. */

async Task<string> LerArquivoAsync()
{
    var texto = await File.ReadAllTextAsync("arquivo.txt");
    return texto;
}

async Task GerarRelatorioAsync(CancellationToken cancellationToken)
{
    try
    {
        cancellationToken.ThrowIfCancellationRequested();
        await Task.Delay(5000, cancellationToken); // Simula um atraso de 5 segundos
                                                   // Simulando a geração de um relatório
        var texto = await LerArquivoAsync(cancellationToken);
        Console.WriteLine(texto);
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("A geração do relatório foi cancelada.");
    }
}

await Task.WhenAll(GerarRelatorioAsync(), LerArquivoAsync());

/* ao utilizar Task.WhenAll, as duas tarefas GerarRelatorioAsync e LerArquivoAsync serão executadas simultaneamente,
e o programa aguardará até que ambas sejam concluídas antes de continuar. Isso é útil para otimizar o desempenho, 
permitindo que várias operações assíncronas sejam executadas em paralelo, sem bloquear a thread principal. */

//-------------------------------------------------------
//realizando cancelamento de uma task
var cts = new CancellationTokenSource();
var token = cts.Token;

var taskCancelavel = Task.Run(() => GerarRelatorioAsync(token));

await Task.Delay(2000).ContinueWith(_ => cts.Cancel()); // Simula um atraso de 2 segundos antes de cancelar a tarefa

//-------------------------------------------------------