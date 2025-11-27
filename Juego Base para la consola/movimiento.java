//by carlos peralta

static void moverjugador(char mov) {
    {
        switch (mov) {
            case 'w': if (jugador.x > 0) jugador.x--; break;
            case 's': if (jugador.x < TAM - 1) jugador.x++; break;
            case 'a': if (jugador.y > 0) jugador.y--; break;
            case 'd': if (jugador.y < TAM - 1) jugador.y++; break;
            case 'x':
                if (jugador.x == enemigo.x && jugador.y == enemigo.y) {
                    enemigo.vida -= jugador.daño;
                    System.out.println("ataque (-" + jugador.daño + ")");
                } else {
                    System.out.println("no hay enemigo ");
                }
                return;
        }
    }

    static void moverenemigo() {
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
            System.out.println("te atacan " + enemigo.daño + ")");
        }
    }
}
