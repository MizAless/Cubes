namespace _Game.Scripts
{
    public class CubeDragger
    {
       private bool _isDragging = false;

       private Cube _draggingCube = null;
       
       public void StartDragging(Cube cube)
       {
           if (_isDragging)
               return;
           
           _isDragging = true;
           _draggingCube = cube;
       }
    }
}