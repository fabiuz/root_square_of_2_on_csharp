namespace raiz_quadrada_de_2_versao_1;

class Program
{
    static void Main(string[] args)
    {
        List<byte> multiplicando = new();
        List<byte> multiplicador = new();
        List<byte> produto = new List<byte>();
        
        multiplicando.Add(1);
        //multiplicando.Add(9);
        
        multiplicador.Add(1);
        //multiplicador.Add(9);

        int indiceAlgarismo = 9;
        
        ulong contador_de_digito = 0;
        
        for (;;)
        {
            int multiplicador_qt_vezes = multiplicador.Count;
            int indice_multiplicador = multiplicador.Count - 1;

            int indice_produto = -1;
            int indice_posicao_soma = 0;

            while (multiplicador_qt_vezes > 0)
            {
                byte vai_um = 0;
                indice_produto += 1;
                
                indice_posicao_soma = indice_produto;
                
                for (var x = multiplicando.Count - 1; x >= 0; x--)
                {
                    var multiplicacao = (byte)(multiplicando[x] * multiplicador[indice_multiplicador] + vai_um);
                    vai_um = (byte)(multiplicacao /10);
                    byte algarismo = (byte)(multiplicacao % 10);

                    if (produto.Count == 0)
                    {
                        produto.Add(0);
                    }
                    
                    var soma = (byte) (produto[indice_posicao_soma] + algarismo);
                    
                    // Se x é igual a zero, devemos adicionar o valor de 'vai_um'.
                    if (x == 0)
                    {
                        if (vai_um != 0)
                        {
                            if (indice_posicao_soma + 1 == produto.Count)
                            {
                                produto.Add(0);
                            }

                            produto[indice_posicao_soma + 1] += vai_um;
                        }
                    }
                    
                    var vai_um_temp = (byte) (soma / 10);
                    var algarismo_temp = (byte) (soma % 10);

                    produto[indice_posicao_soma] = algarismo_temp;
                    if (vai_um_temp != 0)
                    {
                        if (indice_posicao_soma + 1 == produto.Count)
                        {
                            produto.Add(0);
                        }
                        produto[indice_posicao_soma + 1] += vai_um_temp;
                    }

                    indice_posicao_soma += 1;
                    if (indice_posicao_soma == produto.Count)
                    {
                        if (x != 0)
                        {
                            produto.Add(0);
                        }
                    }
                    
                }
                
                indice_multiplicador--;
                multiplicador_qt_vezes--;
            }

            var ultimo_casa = produto.Count - 1;
            if (produto[ultimo_casa] >= 2)
            {
                indiceAlgarismo -= 1;
                if (indiceAlgarismo < 0)
                {
                    System.Console.WriteLine("Fim do número irracional.");
                    System.Console.WriteLine($"Digitos: {contador_de_digito}");
                    return;
                }

                multiplicador[^1] = (byte)indiceAlgarismo;
                multiplicando[^1] = (byte) indiceAlgarismo;
            }
            else
            {
                indiceAlgarismo = 9;
                
                contador_de_digito += 1;
                if (contador_de_digito % 1 == 0)
                {
                    System.Console.WriteLine($"Digitos: {contador_de_digito}");
                    //System.Console.Write($"{multiplicador[^1]}");
                }
                
                multiplicador.Add((byte)indiceAlgarismo);
                multiplicando.Add((byte)indiceAlgarismo);
                //produto.Clear();


            }
            
            for (var indice = 0; indice < produto.Count; indice++)
            {
                produto[indice] = 0;
            }
        }
    }
}
