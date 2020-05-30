//Libreria para generar valores aleatorios con distintas distribuciones
using System;

public class GeneradorVariables {
    //Para determinar que formula vamos a usar en la normal
    // true=primer funcion
    // false=segunda funcion
    private static bool normalUsado = true;


    //LOS METODOS DEL GENERADOR SOLO DEVUELVEN UN VALOR SALVO EL NORMAL QUE DEVUELVE UN ARRAY DE DOS VALORES


    //Instanciacion del generador de  valores aleatorios
    Random rnd = new Random();

    //Distribucion Uniforme
    public double Uniforme(int limiteInferior, int limiteSuperior, double rnd)
    {
        int a = limiteInferior;
        int b = limiteSuperior;

        //Declaración del numero aleatorio
        double valores;
            //Se suma A a un numero aleatorio que se encuentra entre
            //el Delta que se encuentre entre A y B      
            valores = a + rnd * (b - a);
          
        return valores;
    }

    //Distribucion Exponencial (No se utiliza)
    public double Exponencial(double lambda){
        double l = lambda;

        //Declaracion del Array
        double valores;
        //Recorrido del array

            valores = (-1 / l) * (Math.Log(1 - rnd.NextDouble()));

        return valores;
    }

    //Distribucion Poisson (No se utiliza)
    public double[] Poisson(double lambda,  int cantidadValores ){
        double l = lambda;
        int n = cantidadValores;
        //Declaracion de constantes
        //Declaracion del Array
        double[] valores = new double[n];
        double a = Math.Pow(Math.E, -l);
        for (int i = 0; i < n ; i++ ){
            double p = 1;
            double x = -1;
            while (p >= a) {
                double u = rnd.NextDouble();
                p = p * u;
                x = x + 1;
            }
            valores[i] = x;
           
        }
        return valores; 
    }


    //Distribucion Normal
    public double Normal(double random1, double random2, double media, double varianza)
    {
        double u = media;
        double v = varianza;
        double valor;

        //Declaracion del Array
        //double[] valores = new double[2];
        //Declaracion del valor PHI
        double PI = 3.1415926535897931;

        //Declaracion de dos numeros aleatorios RND1 y RND2
        //double rnd1 = rnd.NextDouble();
        //double rnd2 = rnd.NextDouble();
        if (normalUsado)
        {
            //Calculo del primer numero
            valor = (Math.Sqrt(-2 * Math.Log(random1)) * Math.Cos(2 * PI * random2)) * Math.Sqrt(v) + u;
        }
        else
        {
            //Calculo del segundo numero
            valor = (Math.Sqrt(-2 * Math.Log(random1)) * Math.Sin(2 * PI * random2)) * Math.Sqrt(v) + u;
        }

        normalUsado = !normalUsado;
        return valor;
    }
};