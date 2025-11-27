import java.util.Random;
import java.util.Scanner;

class Jugador {
    int vida = 20;
    int daño = 5;
    int x = 0, y = 0;
}

class Enemigo {
    int vida = 10;
    int daño = 3;
    int x, y;

    public Enemigo(int x, int y) {
        this.x = x;
        this.y = y;
    }
}

public class juego {
    static final int TAM = 5;
    static Jugador jugador = new Jugador();
    static Enemigo enemigo = new Enemigo(3, 3);
    static Scanner sc = new Scanner(System.in);

    static void mostrarMapa() {
        for (int i = 0; i < TAM; i++) {
            for (int j = 0; j < TAM; j++) {
                if (jugador.x == i && jugador.y == j)
                    System.out.print("P ");
                else if (enemigo.x == i && enemigo.y == j)
                    System.out.print("E ");
                else
                    System.out.print(". "); 
            }
            System.out.println();
        }
        System.out.println("Vida" + jugador.vida + " | Enemigo: " + enemigo.vida);
    }

    static void moverJugador(char mov) {
        switch (mov) {
            case 'w': if (jugador.x > 0) jugador.x--; break;
            case 's': if (jugador.x < TAM - 1) jugador.x++; break;
            case 'a': if (jugador.y > 0) jugador.y--; break;
            case 'd': if (jugador.y < TAM - 1) jugador.y++; break;
            case 'x':
                if (jugador.x == enemigo.x && jugador.y == enemigo.y) {
                    enemigo.vida -= jugador.daño;
                    System.out.println("Buen ataque");
                } else {
                    System.out.println("No hay enemigo aqui");
                }
                return;
        }
    }

    static void moverEnemigo() {
        Random r = new Random();
        int mov = r.nextInt(4);
        switch (mov) {
            case 0: if (enemigo.x > 0) enemigo.x--; break;
            case 1: if (enemigo.x < TAM - 1) enemigo.x++; break;
            case 2: if (enemigo.y > 0) enemigo.y--; break;
            case 3: if (enemigo.y < TAM - 1) enemigo.y++; break;
        }

        if (enemigo.x == jugador.x && enemigo.y == jugador.y) {
            jugador.vida -= enemigo.daño;
            System.out.println("El enemigo te ataca (-" + enemigo.daño + ")");
        }
    }

    public static void main(String[] args) {
        System.out.println("base inicial");
        System.out.println("Controles: w=arriba, s=abajo, a=izquierda, d=derecha, x=atacar\n");

        while (jugador.vida > 0 && enemigo.vida > 0) {
            mostrarMapa();
            System.out.print("Mover > ");
            char mov = sc.next().charAt(0);
            moverJugador(mov);
            moverEnemigo();
            if (enemigo.vida <= 0) break;
        }

        if (jugador.vida <= 0) System.out.println("JAA PERDISTE");
        else System.out.println("Genial, ganaste");
        
        sc.close();
    }
}