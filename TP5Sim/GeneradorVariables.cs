//Libreria para generar valores aleatorios con distintas distribuciones
using System;

public class GeneradorVariables {

    //Instanciacion del generador de  valores aleatorios
    Random rnd = new Random();

    //Distribucion Uniforme
    public double[] Uniforme(int limiteInferior, int limiteSuperior, int cantidadValores){
        int a = limiteInferior;
        int b = limiteSuperior;
        int n = cantidadValores;
        //Declaración del Array
        double[] valores = new double[n];
        //Recorrido del array y generacion de valores aleatorios
        for (int i = 0; i < n ; i++ ){
            //Se suma A a un numero aleatorio que se encuentra entre
            //el Delta que se encuentre entre A y B      
            valores[i] = a + rnd.Next(0,b - a);
          
        };    
        return valores;
    }

    //Distribucion Exponencial
    public double[] Exponencial(double lambda, int cantidadValores){
        double l = lambda;
        int n = cantidadValores;
        //Declaracion del Array
        double[] valores = new double[n];
        //Recorrido del array
        for (int i = 0; i < n ; i++ ){
            valores[i] = (-1 / l) * (Math.Log(1 - rnd.NextDouble()));         
        };
        return valores;
    }

    //Distribucion Poisson
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
    public double[] Normal(double media, double varianza , int cantidadValores ){
        double u = media;
        double v = varianza;
        int n = cantidadValores;
        //Declaracion del Array
        double[] valores = new double[n];
        //Declaracion del valor PHI
        double PI = 3.1415926535897931;
        for (int i = 0; i < n ; i += 2){
            //Declaracion de dos numeros aleatorios RND1 y RND2
            double rnd1 = rnd.NextDouble();
            double rnd2 = rnd.NextDouble();
            //Calculo del primer numero
            valores[i] = (Math.Sqrt(-2 * Math.Log(rnd1)) * Math.Cos(2 * PI * rnd2 )) * Math.Sqrt(v) + u ;
            //Calculo del segundo numero
            valores[i + 1] = (Math.Sqrt(-2 * Math.Log(rnd1)) * Math.Sin(2 * PI * rnd2 )) * Math.Sqrt(v) + u ;

        }       
        return valores;
    }
};