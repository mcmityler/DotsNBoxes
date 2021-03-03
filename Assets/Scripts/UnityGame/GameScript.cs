using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [Range(2.0F, 8.0F)] //slider in inspector, goes from 2 - 8
    [SerializeField] private int boardSize = 4; //Board size in game, (ie 4x4, max it can go is 8x8 before it goes off screen)
    Dictionary<string, Button> colButtons = new Dictionary<string, Button>(); //dictionary of buttons in column named by their position on a graph, ie 00, 01, 10 (first number is x value, 2nd num is y value ie xy or (0,0) would be 00)
    Dictionary<string, Button> rowButtons = new Dictionary<string, Button>();//dictionary of buttons in row named by their position on a graph like above
    Dictionary<string, bool> rowClicked = new Dictionary<string, bool>();//dictionary of bools in row named by their position on a graph, tells whether buttons been pressed or not
    Dictionary<string, bool> colClicked = new Dictionary<string, bool>();//dictionary of bools in column named by their position on a graph, tells whether buttons been pressed or not
    public Button cornerButton; //reference to the prefab for each button.
    public GameObject box; //reference to prefab for the middle of the button (what changes colour to show who scored the point). also holds what buttons center takes to change colour.
    GameObject canvas; //reference to canvas obj
    public List<GameObject> boxes = new List<GameObject>(); //list of boxed (the center of whats between 4 buttons and what changes colour)

    private bool firstPlayerTurn = false; //Holds position of whos turn it is 

    [SerializeField] Text turnbox; //reference to textbox display; displaying whos turn it is
    private string player1name = "player 1"; //first players name / default will be "player 1"
    private string player2name = "player 2";//second players name / default will be "player 2"
    [SerializeField] Text totalScoreText; //reference to textbox display of the total over multiple games score.
    [SerializeField] Text ggText; // reference to textbox display the game over message, telling the players the outcome of the game. also HOLDS RESTART/QUIT BUTTON.
    [SerializeField] Text player1Textbox; //reference to retrieve the player 1s name INPUT
    [SerializeField] Text p1ScoreTextbox; //reference to textbox display player 1s name and score.
    [SerializeField] Text p2ScoreTextbox;//reference to textbox display player 2s name and score.
    [SerializeField] Text player2Textbox; //reference to textbox retrieve the player 2s name INPUT
    [SerializeField] Text sliderText; //reference to slider text box to display what number the slider is at
    [SerializeField] Slider slider; //slider for the player to select the size of board that they would want.
    [SerializeField] GameObject gamePanel; // reference to panel that displays player 1 & 2 input and the board size and start button. 
    private int p1Score = 0; //second players points in current game
    private int p2Score = 0; //second players points in current game
    private int p1Total = 0;//how many games player 1 has won in total
    private int p2Total = 0;//how many games player 2 has won in total

    private enum gamestate {RESTART, PLAYING, GAMEOVER}; //Enum for game state / what point the game is currently at.
    private gamestate currentGamestate= gamestate.RESTART; //Actual reference to current game state

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas"); //give canvas reference to the canvas obj
    }
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.Escape)) // if escape pressed exit application.
        {
            QuitButton();
        }
        if(currentGamestate == gamestate.PLAYING){ // check gamestate to see if being played
            if(firstPlayerTurn){ //check whos turn it is and display it in the turn text box.
                turnbox.text = player1name + "'s turn";
            }else if(!firstPlayerTurn){
                turnbox.text = player2name + "'s turn";
            }
            //Display players current game score (how many boxes they have got in the CURRENT game)
            p1ScoreTextbox.text = player1name + "'s score: " + p1Score.ToString();
            p2ScoreTextbox.text = player2name + "'s score: " + p2Score.ToString();
        }else if(currentGamestate == gamestate.RESTART){  //check if not playing/not started.
            sliderText.text = slider.value.ToString(); //sets the value of the slider to the text box next to it so you know what position youre at on the slider 
        }
        if(currentGamestate == gamestate.GAMEOVER){  //check if gamestate is gameover
            if(p1Score > p2Score){ //give out points depending on scores in the CURRENT game.
                ggText.text = player1name + " wins!";
                p1Total ++;
            }
            if(p2Score > p1Score){
                ggText.text = player2name + " wins!";
                p2Total ++;
            }
            if(p2Score == p1Score){
                ggText.text = player2name + " & "+ player1name +" tie";
            }
            ggText.gameObject.SetActive(true); //set gameover panel to true so you can click restart button/quit button/see game over message 
            totalScoreText.text =  player1name + " - "+ p1Total.ToString() + "      "+ player2name + " - " + p2Total.ToString(); //set text string to show scores
            currentGamestate = gamestate.RESTART; //set gamestate to restart state.
        }
        
    }

    public void StartButton(){ //function called upon clicking the start game button
        player1name = player1Textbox.text; //get players input for their name
        if(player1name == ""){ //set default name if there isnt a name inputed
            player1name = "player 1";
        }
        player2name = player2Textbox.text; //get players input for their name
        if(player2name == ""){//set default name if there isnt a name inputed
            player2name = "player 2";
        }
        boardSize = (int) slider.value; //get how big the board is from the slider.
        gamePanel.SetActive(false); //make the panel with inputs invisable
        CreateGame(boardSize); //call function to create board
        currentGamestate = gamestate.PLAYING; //set game state to playing game
        
    }
    public void RestartButton(){ //function called upon clicking restart button. 
        foreach(GameObject _b in GameObject.FindGameObjectsWithTag("button")){ //find all objects with the tag button to delete them from the scene
            Destroy(_b);
        }
        foreach(GameObject _box in boxes){ ///find all boxes and destroy them
            Destroy(_box);
        }
        colButtons = new Dictionary<string, Button>(); //create new col dictionary
        rowButtons = new Dictionary<string, Button>(); //create new row dictionary
        rowClicked = new Dictionary<string, bool>(); //create new row clicked dictionary
        colClicked = new Dictionary<string, bool>();//create new col clicked dictionary
        boxes = new List<GameObject>(); //create new box list.
        ggText.gameObject.SetActive(false); //make gg text disappear.
        p1Score = 0; //set CURRENT game scores to 0
        p2Score = 0;//set CURRENT game scores to 0
        CreateGame(boardSize); //create new board!
    }
    public void QuitButton(){ // quit func to exit application.
        Application.Quit();
    }
    void CreateGame(int _boardSize){ //create board function
        if((Random.Range(0, 2) == 0)){ //randomize who gets to go first, randoms a number 0 or 1 to choose turns.
            firstPlayerTurn = true;
        }else{
            firstPlayerTurn = false;
        } 
        //create dictionary of ROW of buttons and set their placement, name, and other
        for(int b = 0; b<_boardSize + 1; b++){
            for(int i = 0; i < _boardSize ; i++){
                Button button =  Instantiate(cornerButton, Vector3.zero, Quaternion.identity) as Button; //make new button from prefab
                var rectTransform = button.GetComponent<RectTransform>(); //get reference of buttons rectTransform
                rectTransform.SetParent(canvas.transform); //make button child of canvas
                rectTransform.offsetMin = Vector2.zero; //make off set 0
                rectTransform.offsetMax = Vector2.zero;//make off set 0
                rectTransform.sizeDelta = new Vector2(82,20); //set buttons size
                //set buttons position on canvas
                rectTransform.position = new Vector2(canvas.GetComponent<RectTransform>().rect.width/2 + (boardSize/2*80) - i * 80,canvas.GetComponent<RectTransform>().rect.height/2 + (boardSize/2*80) + 35 - b * 80);
                button.name = b.ToString() + i.ToString(); //set name of button
                rowButtons.Add(b.ToString() + i.ToString(), button); //add button to dictionary with button name
                rowButtons[b.ToString() + i.ToString()].onClick.AddListener (() => ButtonClicked(button, true)); //add listener to button so it knows when its clicked and which is clicked.
                rowClicked.Add(b.ToString() + i.ToString(),false); //add if button is clicked to dictionary with button name
            }
        }
        //create dictionary of COLUMNS of buttons and set their placement, name, and other
          for(int b = 0; b<_boardSize ; b++){
            for(int i = 0; i < _boardSize + 1; i++){
                Button button = Instantiate(cornerButton, Vector3.zero, Quaternion.identity) as Button; //make new button from prefab
                var rectTransform = button.GetComponent<RectTransform>(); //get reference of buttons rectTransform
                rectTransform.SetParent(canvas.transform);//make button child of canvas
                rectTransform.offsetMin = Vector2.zero;//make off set 0
                rectTransform.offsetMax = Vector2.zero;//make off set 0
                rectTransform.sizeDelta = new Vector2(82,20); //set buttons size
                //set buttons position on canvas
                rectTransform.position = new Vector2(canvas.GetComponent<RectTransform>().rect.width/2 + (boardSize/2*80) + 35 - i * 80,canvas.GetComponent<RectTransform>().rect.height/2 + (boardSize/2*80) - b * 80);
                //set rotation of buttons.
                rectTransform.eulerAngles = new Vector3(rectTransform.transform.eulerAngles.x, rectTransform.transform.eulerAngles.y, 90);
                button.name = b.ToString() + i.ToString();//set name of button
                colButtons.Add(b.ToString() + i.ToString(), button); //add button to dictionary with button name
                colButtons[b.ToString() + i.ToString()].onClick.AddListener (() => ButtonClicked(button, false));//add listener to button so it knows when its clicked and which is clicked.
                colClicked.Add(b.ToString() + i.ToString(),false);//add if button is clicked to dictionary with button name
            }
        }
        //create list of BOXES and set their placement, name, and other
        for(int b = 0; b<_boardSize; b++){
            for(int i = 0; i < _boardSize ; i++){
                if(colButtons.ContainsKey(b.ToString() + (i + 1).ToString()) && rowButtons.ContainsKey((b + 1).ToString() + i .ToString())){ //check that the boxes are within the correct buttons.
                    GameObject _box = Instantiate(box, Vector3.zero, Quaternion.identity) as GameObject;//make box obj from prefab
                    var _rectTransform = _box.GetComponent<RectTransform>(); //get reference of buttons rectTransform
                    _rectTransform.SetParent(canvas.transform);//make button child of canvas
                    _rectTransform.offsetMin = Vector2.zero;//make off set 0
                    _rectTransform.offsetMax = Vector2.zero;//make off set 0
                    _rectTransform.SetAsFirstSibling(); //make sure box is behind buttons
                    _rectTransform.sizeDelta = new Vector2(75,75);//set buttons size
                    //set buttons position on canvas
                    _rectTransform.position = new Vector2(canvas.GetComponent<RectTransform>().rect.width/2 + (boardSize/2*80) - i * 80,canvas.GetComponent<RectTransform>().rect.height/2 + (boardSize/2*80) - b * 80);;
                    //add buttons that are surrounding boxes to list within the box prefab.
                    _box.GetComponent<BoxScript>().boxLines(rowButtons[b.ToString() + i.ToString()]); 
                    _box.GetComponent<BoxScript>().boxLines(rowButtons[(b+1).ToString() + i.ToString()]);
                    _box.GetComponent<BoxScript>().boxLines(colButtons[b.ToString() + i.ToString()]);
                    _box.GetComponent<BoxScript>().boxLines(colButtons[b.ToString() + (i + 1).ToString()]);
                    _box.tag = "unchecked";//tag box that it hasnt been taken for points.
                    _box.name = (b + i).ToString(); //name box given x,y coords on board.
                    boxes.Add(_box); //add box to boxes list.
                }
            }
        }
        ggText.GetComponent<RectTransform>().SetAsLastSibling(); // make the gg text be on top of everything else. 
    }
    public void ButtonClicked(Button _b, bool _row){ //function is called when row or column button is pressed, passes button pressed & bool for whether it is a row or col button.
        bool alreadyClicked = false; //temp holds if it has been pressed in the past
        if(_row && rowClicked[_b.name] != true){  //check it it is a ROW button && if it HASNT been pressed.
            rowClicked[_b.name] = true;
        }else if(!_row && colClicked[_b.name] != true){//check it it is a COL button && if  it HASNT been pressed.
            colClicked[_b.name] = true;
        }else{ //tells script that this button has already been pressed in the past
            alreadyClicked = true;
        }
        if(!alreadyClicked){ //if button hasnt been clicked then change the buttons colour and check if the box needs to be filled in.
             if(firstPlayerTurn){
                _b.GetComponent<Image>().color = Color.blue;
            }else if(!firstPlayerTurn){
                _b.GetComponent<Image>().color = Color.red;
            }
            CheckBoxes(); //check if box needs to be filled in. 
        }
    }
    private void CheckBoxes(){
        
        bool didCheck = false; //holds whether there are any unclaimed boxes left
        bool[] clicked = new bool[4]; //temp array to hold bools of whether its been clicked.
        bool pointGained = false; //holds whether you scored a point or not, basically to change turns, if point gained dont change turn.
        
        foreach(GameObject box in boxes ){ //go through list of boxes in game
            if(box.tag == "unchecked"){ //make sure box hasnt been claimed already
                List<Button> temp =  box.GetComponent<BoxScript>().buttonList; //temp list of buttons surrounding box
                int counter = 0; //counter to distinguish buttons between row and column
                foreach(Button but in temp){
                    if(counter < 2){ //first two buttons stored on box are always ROW buttons
                        //check and add whether button was clicked to temp list
                        if(rowClicked[but.name]){
                            clicked[counter] = true; 
                        }else{
                             clicked[counter] = false ;
                        }
                    }else if (counter < 4){//second two buttons stored on box are always COL buttons
                        //check and add whether button was clicked to temp list
                        if(colClicked[but.name]){
                            clicked[counter] = true; 
                        }else{ 
                            clicked[counter] = false;
                        }

                    }else{ //button shouldnt exist
                        Debug.Log("button shouldnt exist");
                    }   
                    counter ++;
                    
                }
                if(clicked[0] && clicked[1] && clicked[2] && clicked[3]){ // if all buttons in temp clicked array were pressed do this.
                    box.GetComponent<BoxScript>().ButtonSurrounded(firstPlayerTurn); //pass to box script to change its colour and depending on whos turn it is.
                    pointGained = true; //point is gained
                    //add points to whoevers turns score.
                    if(firstPlayerTurn){ 
                        p1Score++;
                    }else if(!firstPlayerTurn){
                        p2Score++;
                    }
                    
                }
                
            }
        }
        //if point isnt gained change
        if(!pointGained){
                firstPlayerTurn = !firstPlayerTurn;
        }
        //check if there are any unchecked boxes left
        foreach(GameObject box in boxes ){
            if(box.tag == "unchecked"){
                didCheck = true;
                break;
            }
        } 
        //if no more unchecked boxes left end game.
        if(!didCheck && currentGamestate != gamestate.GAMEOVER){
            currentGamestate = gamestate.GAMEOVER;
        }else{
            didCheck = false;
        }
        
    }
    
}
