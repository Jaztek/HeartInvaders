using UnityEngine;

public static class RandomPosition 
{
    public static Vector2 randomPosition()
    {
        bool isLeft = Random.Range(0, 2) == 0 ? true : false;
        int rangePos = Random.Range(0, 4);
        if (isLeft)
        {
            // las x son negativas
            if(rangePos == 0){
                  return generatePosition( -2f, -7f, 3f, 4f);
            } else if(rangePos == 1 || rangePos == 2){
                return generatePosition( -6f, -7f, -3f, 3f);
            } else {
                return generatePosition( -2f, -7f, -3f, -4f);
            }
        }else{
            if(rangePos == 0){
                return generatePosition( 2f, 7f, 3f, 4f);
            } else if(rangePos == 1 || rangePos == 2){
                return generatePosition( 6f, 7f, -3f, 3f);
            } else {
               return generatePosition( 2f, 7f, -3f, -4f);
            }
        }
    }

    private static Vector2 generatePosition(float minX, float maxX, float minY, float maxY){

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        //UnityEngine.Debug.Log("x=" + x + " y=" + y);

        return new Vector2(x, y);
    }
     
}