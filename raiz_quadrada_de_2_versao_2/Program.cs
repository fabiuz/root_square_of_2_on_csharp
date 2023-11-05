///
/// Programa:   raiz_quadrada_de_2_versao_2
/// Autor:      Fábio Moura de Oliveira
/// Data:       04/11/2023
/// Descrição:
///     Gera todos os dígitos da raiz quadrada de 2.
/// Observação:
///     Conforme a matemática, os dígitos de uma raiz quadrada é infinito, será???
/// Técnica utilizada pra gerar os dígitos da raiz quadrada, descrita em:
/// https://www.freecodecamp.org/news/find-square-root-of-number-calculate-by-hand/

using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace raiz_quadrada_de_2_versao_2;

using System.Collections;

public class Program
{
    static void retirar_zero_inicial(ref List<byte> lista_1)
    {
        while (lista_1.IndexOf(0) == 0)
        {
            lista_1.RemoveAt(0);
        }
    }
    
    
    static bool subtracao(in List<byte> minuendo, in List<byte> subtraendo, ref List<byte>diferenca)
    {
        var minuendo_temp = new List<byte>(minuendo.Count);
        minuendo_temp.Clear();
        minuendo_temp.AddRange(minuendo);

        // Vamos garantir que as duas listas, tenha o mesmo tamanho.
        while (minuendo_temp.Count < subtraendo.Count)
        {
            minuendo_temp.Insert(0, 0);
        }

        var subtraendo_temp = new List<byte>(subtraendo.Count);
        subtraendo_temp.Clear();
        subtraendo_temp.AddRange(subtraendo);

        // Vamos garantir que as duas listas, tenha o mesmo tamanho.
        while (subtraendo_temp.Count < minuendo_temp.Count)
        {
            subtraendo_temp.Insert(0, 0);
        }
        
        // No momento, as duas lista tem que ser do mesmo tamanho.
        if (minuendo.Count != subtraendo_temp.Count)
            return false;
        
        // Minuendo sempre tem que ser maior que subtraendo.
        if (menor_igual_maior(in minuendo, in subtraendo) == -1)
            return false;
        
        // Se chegarmos aqui, minuendo é maior ou igual ao subtraendo, aqui, iremos subtrair
        // subtraendo de minuendo.
        
        // Ao efetuar a subtração, pode acontecer do dígito do minuendo ser menor que o dígito
        // do subtraendo, neste caso, devemos pegar 1 unidade a esquerda, ao fazermos isto,
        // devemos armazenar o novo valor no minuendo. Entretanto, não quero modificar o minuendo,
        // conforme a assinatura da função indicar, então, estou criando uma variável temporária.

        diferenca.Clear();
        diferenca.AddRange(minuendo);

        // No for seguinte, iremos fazer a subtração da direita pra esquerda,
        // se o dígito do minuendo 
        var ultimo_indice = minuendo_temp.Count - 1;
        for (; ultimo_indice >= 0; ultimo_indice -= 1)
        {
            var resto = minuendo_temp[ultimo_indice] - subtraendo_temp[ultimo_indice];
            
            // No if abaixo, se o dígito atual do minuendo é menor que o subtraendo, 
            // devemos pegar emprestado.
            if (Math.Sign(resto) == -1)
            {
                // Pra pegar emprestado, devemos percorrer da direita pra esquerda,
                // até encontrar um dígito maior que 0, se encontrarmos, devemos
                // diminuir 1 unidade desta posição e em seguida, retornar
                // da esquerda pra direita, adicionando +10 unidades pra próxima e retirando
                // 1 unidade até chegar, a posição do índice.
                var indice_emprestado = ultimo_indice - 1;
                var pegou_emprestado = false;
                while (indice_emprestado >= 0)
                {
                    if (minuendo_temp[indice_emprestado] > 0)
                    {
                        // Se ao sair do loop, devemos verificar se realmente, houve algo emprestado,
                        // pois, senão, isto indica um erro.
                        pegou_emprestado = true;
                        while (indice_emprestado < ultimo_indice)
                        {
                            minuendo_temp[indice_emprestado] -= 1;
                            indice_emprestado += 1;
                            minuendo_temp[indice_emprestado] += 10;
                        }

                        resto = minuendo_temp[ultimo_indice] - subtraendo_temp[ultimo_indice];
                        break;
                    }

                    indice_emprestado -= 1;
                }

                // Se chegamos até aqui, quer dizer, que o dígito do minuendo é menor que 
                // o dígito do subtraendo. Então, pra resolver isto, devemos pegar
                // emprestado, na próxima casa, que tem um valor diferente de zero.
                // Se conseguimos pegar este empréstimo, a variável 'pegou_emprestado'
                // será definida pra 'true', se isto, não ocorreu, há algum 'bug' no código.
                
                if (pegou_emprestado == false)
                {
                    throw new Exception("Dígito do minuendo, menor que o dígito do subtraendo," +
                                        " entretanto, não conseguimos pegar emprestado em nenhuma das " +
                                        " casas à esquerda");
                }
            }

            diferenca[ultimo_indice] = (byte) resto;
        }

        return true;
    }
    
    

    /// <summary>
    /// A função menor_igual_maior retorna se um número, formado pelos dígitos da lista_1 é menor, igual ou maior,
    /// que o número formado pelos dígitos da lista_1.
    /// A função retorna -1, se o número da lista_1 é menor que o número da lista_2.
    /// A função retorna  0, se o número da lista_1 é igual ao número da lista_2.
    /// A função retorna  1, se o número da lista_1 é mairo que o número da lista_2.
    ///
    /// A função retorna 0, somente, se a quantidade de dígitos da lista_1 for igual à quantidade de dígitos
    /// da lista_2, se o número formado pelo dígitos da esquerda pra direita da lista_1 for igual ao número
    /// formado pelos dígitos da esquerda pra direita da lista_2.
    /// 
    /// </summary>
    /// <param name="lista_1">Lista de dígitos que forma o primeiro número a ser comparado.</param>
    /// <param name="lista_2">Lista de dígitos que forma o segundo número a ser comparado.</param>
    /// <returns></returns>
    static int menor_igual_maior(in List<byte> lista_1, in List<byte> lista_2)
    {
        if (lista_1.Count < lista_2.Count)
            return -1;
        
        if (lista_1.Count > lista_2.Count)
            return 1;
        
        if (lista_1.Count == 0 && lista_2.Count == 0)
            return 0;
        
        // Se chegarmos aqui, a quantidade de dígitos da lista_1 é igual à quantidade de dígitos da lista_2:
        // Então, devemos verificar da esquerda pra direita, digito a digito, se um dígito é menor, igual, ou 
        // maior que outro.
        for (var indice = 0; indice < lista_1.Count(); indice++)
        {
            if (lista_1[indice] < lista_2[indice])
                return -1;
            if (lista_1[indice] > lista_2[indice])
                return 1;
        }

        // Se chegarmos aqui, quer dizer, que os dígitos da lista_1, da esquerda pra direita
        // é igual aos dígitos da lista_2, da esquerda pra direita.
        return 0;
    }
    
    
    /// <summary>
    /// Multiplica cada dígito que está no multiplicando com cada dígito que está
    /// no multiplicador, e retorna o produto.
    /// Isto comporta igual a multiplicação de dois números quaisquer, a diferença
    /// aqui, é possível multiplicar números com milhares de dígitos.
    /// No momento, somente multiplica números inteiros.
    /// </summary>
    /// <param name="fator_1"></param>
    /// <param name="fator_2"></param>
    /// <param name="produto"></param>
    public static void multiplicador(
        in List<byte> fator_1,
        in List<byte> fator_2,
        ref List<byte> produto)
    {
        int qt_de_digitos_fator_1 = fator_1.Count();
        int qt_de_digitos_fator_2 = fator_2.Count();
        
        // Se uma das lista está vazia, o produto é zero.
        if (qt_de_digitos_fator_1 == 0 || qt_de_digitos_fator_2 == 0)
        {
            produto.Clear();
            produto.Add(0);
            return;
        }
        
        // Pra multiplicar devemos, dispor os números alinhados à direita,
        // onde a casa da unidades do multiplicando esteja sobre a casa
        // das unidades do multiplicador, como nossa lista de dígitos, começa
        // no índice '0', devemos identificar o último índice da lista,
        // pra na hora de multiplicar, utiliza as posições corretas.
        int ultimo_indice_fator_1 = qt_de_digitos_fator_1 - 1;
        int ultimo_indice_fator_2 = qt_de_digitos_fator_2 - 1;
        
        // Apaga a lista onde ficará o resultado da multiplicação
        produto.Clear();
        
        // Na lista 'produto', os dígitos serão inseridos da esquerda
        // pra direita, diferentemente, da multiplicação, eu queria
        // fazer isto com a lista de fatores, entretanto, não achei bem
        // interessante, pois ficaria um pouco confuso.
        // A ideia é que quando inserimos, o índice da lista começa em '0',
        // ao inserir, o próximo dígito fica na posição '1', entretanto,
        // se eu for fazer isto, da direita pra esquerda, eu teria que calcular
        // toda vez, e ficaria bem mais complicado.
        // Quando todos os dígitos de uma lista foram multiplicado por um único dígito
        // da outra lista, ao avançarmos pra o próximo dígito da outra lista,
        // simplesmente, é avançar uma posição da esquerda pra direita, se eu fosse
        // fazer isto da direita pra esquerda, teria que calcular quantas ítens há
        // na lista, pra depois fazer o ajuste.
        int indice_produto = -1;
        
        
        
        // Pra multiplicar é bem simples, como cada dígito de uma lista tem
        // que ser multiplicado por cada dígito de uma segunda lista, no final
        // teremos digito x digitos multiplicações. Pra isto, iremos utilizar
        // um loop, utilizando as variáveis: qt_de_digitos_fator_1 e
        // qt_de_digitos_fator_2.

        int indice_fator_2 = ultimo_indice_fator_2;
        while (qt_de_digitos_fator_2 > 0)
        {
            // Antes de cada multiplicação de um dígito, por todos os dígitos
            // devemos reposicionar o índice onde o próximo dígito do 'produto'
            // será colocado e adicionado ao que já existe lá.
            indice_produto += 1;

            // No for seguinte, ao percorrer cada dígito, o produto será colocado
            // em uma posição específica na lista 'produto', a variável serve
            // pra indicar isto. Ela será incrementada toda vez que houver uma iteração
            // do loop, isto indica que o produto de um dígito da lista com o dígito
            // de outra lista foi feito.
            var indice_produto_atual = indice_produto;
            
            byte digito_vai_um = 0;
            
            // Iremos multiplicar os digitos da lista, da direita pra esquerda
            // igualzinho à multiplicação por isto, devemos começar do final da lista.
            for (var indice_fator_1 = ultimo_indice_fator_1; indice_fator_1 >= 0; indice_fator_1--)
            {
                // Se o dígito atual do fator_2 é '0' (zero), todos os dígitos serão zerados, então, devemos
                // sair do loop.
                if (fator_2[indice_fator_2] == 0)
                {
                    break;
                }
                
                var multiplicacao_1 = (byte)(fator_1[indice_fator_1] * fator_2[indice_fator_2] + digito_vai_um);
                
                // A lista de dígitos, somente comporta números de '0' a '9', ou seja, números com '1' dígito.
                // Ao multiplicar, na sentença acima, dois números de '1' dígito cada, pode ocorrer de 
                // termos um número com '2' dígitos, devemos então, separar o dígito da dezena e da unidade.
                digito_vai_um = (byte) (multiplicacao_1 / 10);
                
                // O dígito da unidade é sempre um número entre '0' e '9'.
                var digito_unidade = (byte) (multiplicacao_1 % 10);
                
                // Agora, iremos adicionar o dígito da unidade, à posição atual da lista 'produto'.
                // Primeiro, devemos verificar se a lista está vazia.
                if (produto.Count == 0 || indice_produto_atual == produto.Count)
                {
                    produto.Add(0);
                }

                // Aqui, iremos somar o dígito atual com o dígito da posição atual da lista 'produto'.
                var soma = (byte) (produto[indice_produto_atual] + digito_unidade);
                
                // Se este é o último dígito da lista do fator_1, devemos adicionar o vai_um atual,
                // pois, senão, ao terminar o loop, o valor de 'vai_um' ficará perdido, isto descobri
                // por causa que fiz testes.
                if (indice_fator_1 == 0)
                {
                    if (digito_vai_um != 0)
                    {
                        // Verifica se a posição atual é a última da lista, se for, devemos
                        // aumentar a lista.
                        if (indice_produto_atual + 1 == produto.Count)
                        {
                            produto.Add(0);
                        }
                        
                        // Adiciona o valor a próxima posição.
                        produto[indice_produto_atual + 1] += digito_vai_um;
                    }
                }
                
                // Ao somarmos o valor da variável 'digito_unidade' com o valor da posição atual
                // da lista 'produto', pode acontecer de termos um número com 2 dígitos, como a lista
                // somente armazena números com 1 dígito, devemos ajustar o valor que está na posição
                // atual de 'produto'.
                // Pra isto, iremos separar o dígito da casa da dezena e da casa da unidade.
                var soma_digito_dezena = (byte) (soma / 10);
                var soma_digito_unidade = (byte) (soma % 10);
                
                // Ao separar, o dígito da casa da unidade da soma,
                // será o novo dígito na posição atual da lista 'produto'.
                produto[indice_produto_atual] = soma_digito_unidade;
                
                // Em seguida, o dígito da casa da dezena da soma, será somado
                // a próxima posição da lista 'produto'.
                // Pra isto, primeiro devemos verifica se a dezena é diferente de zero,
                // se for, devemos verificar se estamos atualmente no final da lista,
                // se sim, devemos inserir uma nova posição.
                if (soma_digito_dezena != 0)
                {
                    // Como a lista é base zero, devemos verificar, se a próxima posição
                    // é igual à quantidade de ítens na lista, se sim, quer dizer, que estamos
                    // atualmente, no final da lista.
                    if (indice_produto_atual + 1 == produto.Count)
                    {
                        produto.Add(0);
                    }

                    produto[indice_produto_atual + 1] += soma_digito_dezena;
                }
                
                // Agora, acabamos de multiplicar o digito de uma lista com um dígito da outra 
                // lista, devemos apontar pra a próxima posição, onde o produto será
                // adicionado.
                indice_produto_atual += 1;
            }
            qt_de_digitos_fator_2 -= 1;
            indice_fator_2 -= 1;
        }

        produto.Reverse();
        
        // System.Console.WriteLine("\nDigitos:");
        // foreach(var valor in produto)
        // {
        //     System.Console.Write($"{valor},");
        // }
        //
    }
    
    static void Main(string[] args)
    {
        //var fator_1 = new List<byte>{9,9};
        //var fator_2 = new List<byte>{9,9};
        //var produto = new List<byte>();

        // Armazena os dígitos da raiz quadrada localizada.
        var digitos = new List<byte>();

        // Armazena quantas vezes cada dígito apareceu.
        var qt_vz_por_digito = new ulong[10];
        
        // Armazena os dígitos do número que iremos fazer a extração da raiz quadrada.
        // Se este número for quadrado perfeito, 
        var numero = new List<byte>();
        
        // Armazena o número 2.
        numero.Add(2);
        
        // A lista de dígitos do número que iremos extrar a raíz quadrada deve ter um quantidade par de dígitos,
        // se não houver esta quantidade par de dígitos, devemos inserir um dígito '0' no ínicio da lista.
        if (numero.Count % 2 != 0)
        {
            numero.Insert(0, 0);
        }

        var digito_temp = new List<byte> {0, 9};
        var numero_temp = new List<byte> { numero[0], numero[1]};
        var produto = new List<byte>();
        
        for (var indice = 9; indice >= 0; indice--)
        {
            digito_temp[^1] = (byte)indice;
            
            multiplicador(in digito_temp, in digito_temp, ref produto);

            var numero_comparado = menor_igual_maior(in produto, in numero_temp); 
            

            if (numero_comparado == 0 || numero_comparado == -1)
            {
                digitos.Add((byte)indice);
                qt_vz_por_digito[indice] += 1;
                break;
            }
        }

        var diferenca = new List<byte>();
        subtracao(in numero_temp, in produto, ref diferenca);
        
        // Agora, copia diferença pra o numero.
        numero.RemoveRange(0, diferenca.Count);
        numero.InsertRange(0, diferenca);
        
        // Retira os zeros iniciais.
        while (numero.IndexOf(0) == 0)
        {
            numero.RemoveAt(0);
        }

        // Esta lista, irá armazenar os dígitos da lista 'digitos' multiplicados por '2'.
        var digitos_vezes_2 = new List<byte>();
        
        // Armazena o dígito '2' que será o multiplicador que será utilizado
        // pra multiplicar cada dígito da lista 'digitos' por '2'.
        var multiplicador_2 = new List<byte> {2};

        var multiplicador_de_0_a_9 = new List<byte> {0};

        long qt_de_digitos = 0;
        qt_de_digitos = digitos.LongCount();

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        
        for (;;)
        {
            if (numero.Count == 0)
            {
                System.Console.WriteLine("Todos os dígitos localizados.");
                return;
            }
            
            // Sempre iremos trabalhar em pares.
            numero.Add(0);
            numero.Add(0);
            
            // Obtém a lista de dígitos da raiz quadrada multiplicado por 2.
            multiplicador(in digitos, in multiplicador_2, ref digitos_vezes_2);
            
            // Em seguida, iremos adicionar no final da lista de dígitos 'digitos_vezes_2',
            // um único dígito, e iremos em cada iteração do loop for, substituir este dígito
            // por cada dígito de '9' a '0', em cada iteração, iremos multiplicar
            // 'digitos_vezes' por cada digito de '9' a '0'.
            // Por exemplo:
            // Seja a lista de digitos da variável 'digitos_vezes' igual a: '123x',
            // onde 'x' é um dos dígitos de '0' a '9',
            // Em cada iteração do loop teremos, iremos substituir 'x', por um dos dígitos
            // de '9' a '0'.
            // E esta lista de dígitos será multiplicado pelo dígito que foi substituído, assim:
            // 1239 * 9 
            // 1238 * 8
            // ...
            // 1231 * 1
            // 1230 * 0
            // Em cada iteração do loop, iremos comparar os dígitos do número formado pelo
            // produto da multiplicação com os dígitos do número formado que está na variável
            // 'digitos'. Se o número formado pela multiplicação for menor que o número 
            // que está na variável 'digitos', então, o dígito atual que foi substituido no 
            // lugar de 'x' é o dígito da raiz.
            // Devemos considerar que iremos percorrer os dígitos do maior pro menor,
            // então, na primeira vez que encontrarmos um número menor que o menor número
            // que está na variável 'digitos', que dizer, que achamos, o dígito da raiz.
            digitos_vezes_2.Add(0);
            
            // Indica se o dígito foi encontrado ou não,
            // se não foi encontrado, indica que não é possível extrair mais dígitos da raiz quadrada.
            var digito_encontrado = false;
            
            // Agora, percorrer do maior produto da multiplicação e comparar o número 
            // formado, com o número que está na variável 'digitos'.
            for (var digito_raiz = 9; digito_raiz >= 0; digito_raiz -= 1)
            {
                digitos_vezes_2[^1] = (byte) digito_raiz;
                multiplicador_de_0_a_9[0] = (byte) digito_raiz;
                multiplicador(in digitos_vezes_2, in multiplicador_de_0_a_9, ref produto);
                
                var numero_comparado = menor_igual_maior(in produto, in numero);
                
                if (numero_comparado <= 0)
                {
                    digito_encontrado = true;
                    
                    // Em seguida, devemos subtrair o número atual, do produto encontrado.
                    subtracao(in numero, in produto, ref diferenca);
                    
                    // Agora, copia diferença atual pra o numero.
                    numero.RemoveRange(0, diferenca.Count);
                    numero.InsertRange(0, diferenca);
        
                    // Retira os zeros iniciais.
                    while (numero.IndexOf(0) == 0)
                    {
                        numero.RemoveAt(0);
                    }
                    
                    digitos.Add((byte)digito_raiz);
                    
                    // Contabiliza quantos dígitos foram encontrados.
                    qt_vz_por_digito[digito_raiz] += 1;
                    qt_de_digitos += 1;

                    if (qt_de_digitos % 10000 == 0)
                    {
                        stopwatch.Stop();
                        TimeSpan timeSpan = stopwatch.Elapsed;
                        
                        string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                            timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds,
                            timeSpan.Milliseconds / 10);
                        Console.WriteLine("RunTime " + elapsedTime);
                        
                        System.Console.WriteLine($"Qt de dígitos: {qt_de_digitos}");

                        for (var indice = 0; indice <= 9; indice += 1)
                        {
                            System.Console.WriteLine($"[{indice}]:{qt_vz_por_digito[indice]}");
                        }
                        
                        stopwatch.Restart();
                    }
                    
                    break;
                }
            }

            if (digito_encontrado == false)
            {
                System.Console.WriteLine("Não é possível extrair mais dígitos, " +
                                     "pois, não há nenhum número que multiplicado por ele mesmo, " +
                                     "seja menor ou igual ao número dado.");
                System.Console.WriteLine($"Qt de dígitos encontrados: {qt_de_digitos}");
                return;
            }
        }
        
        
        


        




        // multiplicador(ref fator_1, ref fator_2, ref produto);









        // List<byte> multiplicando = new();
        // List<byte> multiplicador = new();
        // List<byte> produto = new List<byte>();
        //
        // multiplicando.Add(1);
        // //multiplicando.Add(9);
        //
        // multiplicador.Add(1);
        // //multiplicador.Add(9);
        //
        // int indiceAlgarismo = 9;
        //
        // ulong contador_de_digito = 0;
        //
        // for (;;)
        // {
        //     int multiplicador_qt_vezes = multiplicador.Count;
        //     int indice_multiplicador = multiplicador.Count - 1;
        //
        //     int indice_produto = -1;
        //     int indice_posicao_soma = 0;
        //
        //     while (multiplicador_qt_vezes > 0)
        //     {
        //         byte vai_um = 0;
        //         indice_produto += 1;
        //         
        //         indice_posicao_soma = indice_produto;
        //         
        //         for (var x = multiplicando.Count - 1; x >= 0; x--)
        //         {
        //             var multiplicacao = (byte)(multiplicando[x] * multiplicador[indice_multiplicador] + vai_um);
        //             vai_um = (byte)(multiplicacao /10);
        //             var algarismo = (byte)(multiplicacao % 10);
        //
        //             if (produto.Count == 0)
        //             {
        //                 produto.Add(0);
        //             }
        //             
        //             var soma = (byte) (produto[indice_posicao_soma] + algarismo);
        //             
        //             // Se x é igual a zero, devemos adicionar o valor de 'vai_um'.
        //             if (x == 0)
        //             {
        //                 if (vai_um != 0)
        //                 {
        //                     if (indice_posicao_soma + 1 == produto.Count)
        //                     {
        //                         produto.Add(0);
        //                     }
        //
        //                     produto[indice_posicao_soma + 1] += vai_um;
        //                 }
        //             }
        //             
        //             var vai_um_temp = (byte) (soma / 10);
        //             var algarismo_temp = (byte) (soma % 10);
        //
        //             produto[indice_posicao_soma] = algarismo_temp;
        //             if (vai_um_temp != 0)
        //             {
        //                 if (indice_posicao_soma + 1 == produto.Count)
        //                 {
        //                     produto.Add(0);
        //                 }
        //                 produto[indice_posicao_soma + 1] += vai_um_temp;
        //             }
        //
        //             indice_posicao_soma += 1;
        //             if (indice_posicao_soma == produto.Count)
        //             {
        //                 if (x != 0)
        //                 {
        //                     produto.Add(0);
        //                 }
        //             }
        //             
        //         }
        //         
        //         indice_multiplicador--;
        //         multiplicador_qt_vezes--;
        //     }
        //
        //     var ultimo_casa = produto.Count - 1;
        //     if (produto[ultimo_casa] >= 2)
        //     {
        //         indiceAlgarismo -= 1;
        //         if (indiceAlgarismo < 0)
        //         {
        //             System.Console.WriteLine("Fim do número irracional.");
        //             System.Console.WriteLine($"Digitos: {contador_de_digito}");
        //             return;
        //         }
        //
        //         multiplicador[^1] = (byte)indiceAlgarismo;
        //         multiplicando[^1] = (byte) indiceAlgarismo;
        //     }
        //     else
        //     {
        //         indiceAlgarismo = 9;
        //         
        //         contador_de_digito += 1;
        //         if (contador_de_digito % 1 == 0)
        //         {
        //             System.Console.WriteLine($"Digitos: {contador_de_digito}");
        //             //System.Console.Write($"{multiplicador[^1]}");
        //         }
        //         
        //         multiplicador.Add((byte)indiceAlgarismo);
        //         multiplicando.Add((byte)indiceAlgarismo);
        //         //produto.Clear();
        //
        //
        //     }
        //     
        //     for (var indice = 0; indice < produto.Count; indice++)
        //     {
        //         produto[indice] = 0;
        //     }
        // }
    }
}