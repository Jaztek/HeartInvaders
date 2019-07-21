
public static class UtilsStage
{

    public static int getStageRange(int stage)
    {
        if (stage == 0) { return 0; }

        int rest = stage % 5;
        int intervalIni = stage - rest;

        return intervalIni;

    }

     public static float getStageProgres(int stage, long score)
    {
        StagesList stages = StageService.getStages();
        float progresStage = 0;
        StageModel currectStage = stages.stagesList[stage];
        if (stage > 0)
        {
            StageModel previousStage = stages.stagesList[stage - 1];

            long normalDiff = previousStage.maxScore - currectStage.maxScore;
            long normalScore = previousStage.maxScore - score;

            progresStage = 0.2f * normalScore / normalDiff;

        }
        else
        {
            progresStage = 0.2f * score / currectStage.maxScore;
        }

        return progresStage;
    }
}