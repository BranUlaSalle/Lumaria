import java.util.Random;
import java.util.Scanner;

// defino class heredadas
// base principal

class Jugador {
    int vida = 20;
    int daño= 5;
    int x = 0, y = 0;
}

class enemigo {
    int vida = 10;
    int daño = 3;
    int x, y;

    public enemigo(int x, int y) {
        this.x = x;
        this.y = y;
    }
}
static void mostrarmapa() {
    // Recorre el mapa
    for (int i = 0; i < TAM; i++) {
        for (int j = 0; j < TAM; j++) {
            if (jugador.x == i && jugador.y == j)
                System.out.print("P "); // jugador
            else if (enemigo.x == i && enemigo.y == j)
                System.out.print("E "); // enemigo
            else
                System.out.print(". "); 
        }
        System.out.println();
    }
    // Muestra la vida
    System.out.println("❤️ vida jugador: " + jugador.vida + " | 👾 enemigo: " + enemigo.vida);
}
