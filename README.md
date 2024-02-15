
###   Este projeto foi desenvolvido a partir do desafio de código do processo seletivo
e conta com a seguinte proposta:
> Um provedor de endereços retorna endereços apenas com ruas concatenadas, nomes e números em uma única string. Nosso próprio sistema, por outro lado, tem campos específicos para armazenar o nome da rua e o número da rua.
> Portanto, se faz necessário escrever um código simples que processe a entrada e retorne esses campos na saída.
> 
> **Entrada**: string de endereço com os dados concatenados.
> 
> **Saída**: string da rua e string do número da rua.

Utilizei as Expressões Regulares da linguagem C# para atender esses requisítos da melhor forma possível, identificando o número no endereço passado e separando tanto o número quanto a rua em strings diferentes

Visando incrementar o desafio, inclui um menu de usuário com opções que variam entre "Salvar um endereço", "Verificar endereços salvos" e "Encerrar"; além um arquivo de texto que visa simular onde essas informações seriam armazenadas no sistema, e no qual os endereços são ordenados alfabeticamente; e Exceptions para tratamento de erros junto a blocos try/catch.

