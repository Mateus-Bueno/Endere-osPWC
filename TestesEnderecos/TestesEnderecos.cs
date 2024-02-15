using EndereçosPWC.Services;

namespace TestesEnderecos;

public class TestesEnderecos
{

    [Theory]
    [InlineData("Miritiba, 339", 0)]
    [InlineData("Babaçu, 500", 1)]
    [InlineData("Cambuí, 804B", 2)]
    public void DeveSepararCasosSimples(string adress, int i)
    {
        //Arrange
        string[] substrings = Enderecos.SepararStrings(adress);

        string[] expectedStreetOutput = {"Miritiba", "Babaçu", "Cambuí"};
        string[] expectedNumberOutput = {"339", "500", "804B"};

        //Act
        string number = Enderecos.BuscarNumero(substrings);
        string street = Enderecos.RemoverNumero(number, substrings);

        //Assert
        Assert.Equal(expectedStreetOutput[i], street);
        Assert.Equal(expectedNumberOutput[i], number);
    }

    [Theory]
    [InlineData("Rio Branco 23", 0)]
    [InlineData("Quirino dos Santos 23 b", 1)]
    public void DeveSepararCasosComplicados(string adress, int i)
    {
        //Arrange
        string[] substrings = Enderecos.SepararStrings(adress);

        string[] expectedStreetOutput = {"Rio Branco", "Quirino dos Santos"};
        string[] expectedNumberOutput = {"23", "23 b"};

        //Act
        string number = Enderecos.BuscarNumero(substrings);
        string street = Enderecos.RemoverNumero(number, substrings);

        //Assert
        Assert.Equal(expectedStreetOutput[i], street);
        Assert.Equal(expectedNumberOutput[i], number);
    }

    [Theory]
    [InlineData("4, Rue de la République", 0)]
    [InlineData("100 Broadway Av", 1)]
    [InlineData("Calle Sagasta, 26", 2)]
    [InlineData("Calle 44 No 1991", 3)]
    public void DeveSepararCasosComplexos(string adress, int i)
    {
        //Arrange
        string[] substrings = Enderecos.SepararStrings(adress);

        string[] expectedStreetOutput = {"Rue de la République", "Broadway Av", "Calle Sagasta", "Calle 44"};
        string[] expectedNumberOutput = {"4", "100", "26", "No 1991"};

        //Act
        string number = Enderecos.BuscarNumero(substrings);
        string street = Enderecos.RemoverNumero(number, substrings);

        //Assert
        Assert.Equal(expectedStreetOutput[i], street);
        Assert.Equal(expectedNumberOutput[i], number);
    }
}