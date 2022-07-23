public class EndGameState 
{
    private bool isGameEnd;
    private bool isUserLose;
    private bool isBotLose;
    

    public EndGameState()
    {
        this.isGameEnd = false;
        this.isUserLose = false;
        this.isBotLose = false;
    }
    public void setIsGameEnd() { this.isGameEnd = true; }
    public void setIsUserLose() { this.isUserLose = true; }
    public void setIsBotLose() { this.isBotLose = true; }   
    public bool getIsGameEnd() { return this.isGameEnd; }
    public bool getIsUserLose() { return this.isUserLose; } 
    public bool getIsBotLose() { return this.isBotLose; }   

}
