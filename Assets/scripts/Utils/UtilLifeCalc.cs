using System;

public static class UtilsLifeCalc
{
    private static int MAX_LIFES = 10;
    private static int MINUTES_X_LIFE = 5;

    public static void calcLifes()
    {
        int currentLifes = LoadSaveService.game.lifes;
        DateTime dateLast = LoadSaveService.game.dateLastLife;

        if (currentLifes == MAX_LIFES) {return;};

        if(dateLast == DateTime.MinValue){
            // Si estaba a DateTime.MinValue es porque acaba de bajar del maximo, empieza la cuenta atras.
            LoadSaveService.game.dateLastLife = DateTime.Now;
            LoadSaveService.savePlayerLocal();
            return;
        }
        TimeSpan diff = DateTime.Now - dateLast;

        int minutesDiff = (int)diff.TotalMinutes;

        int newLifes = minutesDiff / MINUTES_X_LIFE;

        if (newLifes == 0) {return;};

        UnityEngine.Debug.Log(newLifes + " vidas en " + minutesDiff);
        int lifes = newLifes + currentLifes;

        // Se pone a DateTime.MinValue cuando esta la maximo de vidas.
        LoadSaveService.game.dateLastLife = lifes >= MAX_LIFES? DateTime.MinValue : dateLast.AddMinutes(MINUTES_X_LIFE * newLifes);
        LoadSaveService.game.lifes = lifes > MAX_LIFES ? MAX_LIFES : lifes;
        LoadSaveService.savePlayerLocal();

        UnityEngine.Debug.Log(lifes);
    }
}