public class MiniDungeon {
    static final int TAM = 5;
    static Jugador jugador = new Jugador();
    static Enemigo enemigo = new Enemigo(3, 3);
    static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        System.out.println("=== MINI DUNGEON CONSOLE ===");
        System.out.println("Controles: w=arriba, s=abajo, a=izquierda, d=derecha, x=atacar\n");

        while (jugador.vida > 0 && enemigo.vida > 0) {
            mostrarMapa();
            System.out.print("Mover > ");
            char mov = sc.next().charAt(0);
            moverJugador(mov);
            moverEnemigo();
            if (enemigo.vida <= 0) break;
        }

        if (jugador.vida <= 0) System.out.println("💀 Has sido derrotado...");
        else System.out.println("🏆 ¡Ganaste la batalla!");
    }
