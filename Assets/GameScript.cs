using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [Range(2.0F, 8.0F)]
    [SerializeField] private int boardSize = 4;
    Dictionary<string, Button> colButtons = new Dictionary<string, Button>();
    Dictionary<string, Button> rowButtons = new Dictionary<string, Button>();
    Dictionary<string, bool> rowClicked = new Dictionary<string, bool>();
    Dictionary<string, bool> colClicked = new Dictionary<string, bool>();
    public Button cornerButton;
    public GameObject box;
    GameObject canvas;
    public List<GameObject> boxes = new List<GameObject>();

    private bool firstPlayerTurn = false;

    [SerializeField] Text turnbox;
    private string player1name = "player 1";
    private string player2name = "player 2";
    


    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        //CreateGame(boardSize);
    }

    // Update is called once per frame
    void Update()
    {
        if(playing){
            if(firstPlayerTurn){
                turnbox.text = player1name + "'s turn";
            }else if(!firstPlayerTurn){
                turnbox.text = player2name + "'s turn";
            }
            p1ScoreTextbox.text = player1name + "'s score: " + p1Score.ToString();
            p2ScoreTextbox.text = player2name + "'s score: " + p2Score.ToString();
        }else if(!playing){ 
            sliderText.text = slider.value.ToString();
        }
        if(gameover){
            if(p1Score > p2Score){
                ggText.text = player1name + " wins!";
                p1Total ++;
                ggText.gameObject.SetActive(true);
                
            }
            if(p2Score > p1Score){
                ggText.text = player2name + " wins!";
                p2Total ++;
                
                ggText.gameObject.SetActive(true);
            }
            if(p2Score == p1Score){
                ggText.text = player2name + " & "+ player1name +"Tie";
                ggText.gameObject.SetActive(true);
            }
            totalScoreText.text =  player1name + " - "+ p1Total.ToString() + "      "+ player2name + " - " + p2Total.ToString();
            gameover = false;
        }
         if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitButton();
        }
    }

    [SerializeField] Text totalScoreText;
    [SerializeField] Text ggText;
    [SerializeField] Text player1Textbox;
    [SerializeField] Text p1ScoreTextbox;
    [SerializeField] Text p2ScoreTextbox;
    [SerializeField] Text player2Textbox;
    [SerializeField] Text sliderText;
    [SerializeField] Slider slider;
    [SerializeField] GameObject gamePanel;
    private bool playing = false;
    private int p1Score = 0;
    private int p2Score = 0;
    private int p1Total = 0;
    private int p2Total = 0;

    private bool gameover = false;

    public void StartButton(){
        player1name = player1Textbox.text;
        if(player1name == ""){
            player1name = "player 1";
        }
        player2name = player2Textbox.text;
        if(player2name == ""){
            player2name = "player 2";
        }
        boardSize = (int) slider.value;
        gamePanel.SetActive(false);
        CreateGame(boardSize);
        playing = true;
        
    }
    public void RestartButton(){
        foreach(GameObject _b in GameObject.FindGameObjectsWithTag("button")){
            Destroy(_b);
        }
        foreach(GameObject _box in boxes){
            Destroy(_box);
        }
        colButtons = new Dictionary<string, Button>();
        rowButtons = new Dictionary<string, Button>();
        rowClicked = new Dictionary<string, bool>();
        colClicked = new Dictionary<string, bool>();
        boxes = new List<GameObject>();
        ggText.gameObject.SetActive(false);
        p1Score = 0;
        p2Score = 0;
        CreateGame(boardSize);
    }
    public void QuitButton(){
        Application.Quit();
    }
    void CreateGame(int _boardSize){
        Debug.Log(Random.Range(0, 2));
        if((Random.Range(0, 2) == 0)){
            firstPlayerTurn = true;
        }else{
            firstPlayerTurn = false;
        }
        for(int b = 0; b<_boardSize + 1; b++){
            for(int i = 0; i < _boardSize ; i++){

                Button button =  Instantiate(cornerButton, Vector3.zero, Quaternion.identity) as Button;
                var rectTransform = button.GetComponent<RectTransform>();
                rectTransform.SetParent(canvas.transform);
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;
                //button.transform
                rectTransform.sizeDelta = new Vector2(82,20);
                rectTransform.position = new Vector2(canvas.GetComponent<RectTransform>().rect.width/2 + (boardSize/2*80) - i * 80,canvas.GetComponent<RectTransform>().rect.height/2 + (boardSize/2*80) + 35 - b * 80);
                //button.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(button, true));
                button.name = b.ToString() + i.ToString();
                //lineButtons[b,i] = new GameObject();
                rowButtons.Add(b.ToString() + i.ToString(), button);
                rowButtons[b.ToString() + i.ToString()].onClick.AddListener (() => ButtonClicked(button, true));
                rowClicked.Add(b.ToString() + i.ToString(),false);
                //lineButtons[b,i] = button;
                
               
            }
        }
          for(int b = 0; b<_boardSize ; b++){
            for(int i = 0; i < _boardSize + 1; i++){

                Button button = Instantiate(cornerButton, Vector3.zero, Quaternion.identity) as Button;
                var rectTransform = button.GetComponent<RectTransform>();
                rectTransform.SetParent(canvas.transform);
                rectTransform.offsetMin = Vector2.zero;
                rectTransform.offsetMax = Vector2.zero;
                //button.transform
                rectTransform.sizeDelta = new Vector2(82,20);
                rectTransform.eulerAngles = new Vector3(rectTransform.transform.eulerAngles.x, rectTransform.transform.eulerAngles.y, 90);
                rectTransform.position = new Vector2(canvas.GetComponent<RectTransform>().rect.width/2 + (boardSize/2*80) + 35 - i * 80,canvas.GetComponent<RectTransform>().rect.height/2 + (boardSize/2*80) - b * 80);
                //button.GetComponent<Button>().onClick.AddListener(() => ButtonClicked(button, false));
                button.name = b.ToString() + i.ToString();
                //lineButtons[b,i] = new GameObject();
                colButtons.Add(b.ToString() + i.ToString(), button);
                colButtons[b.ToString() + i.ToString()].onClick.AddListener (() => ButtonClicked(button, false));
                colClicked.Add(b.ToString() + i.ToString(),false);
                //lineButtons[b,i] = button;
                
            }
        }
        for(int b = 0; b<_boardSize; b++){
            for(int i = 0; i < _boardSize ; i++){
                if(colButtons.ContainsKey(b.ToString() + (i + 1).ToString()) && rowButtons.ContainsKey((b + 1).ToString() + i .ToString())){
                    GameObject _box = Instantiate(box, Vector3.zero, Quaternion.identity) as GameObject;
                    var _rectTransform = _box.GetComponent<RectTransform>();
                    _rectTransform.SetParent(canvas.transform);
                    _rectTransform.offsetMin = Vector2.zero;
                    _rectTransform.offsetMax = Vector2.zero;
                    //button.transform
                    _rectTransform.SetAsFirstSibling();
                    _rectTransform.sizeDelta = new Vector2(75,75);
                    _rectTransform.position = new Vector2(canvas.GetComponent<RectTransform>().rect.width/2 + (boardSize/2*80) - i * 80,canvas.GetComponent<RectTransform>().rect.height/2 + (boardSize/2*80) - b * 80);;
                    _box.GetComponent<BoxScript>().boxLines(rowButtons[b.ToString() + i.ToString()]);
                    _box.GetComponent<BoxScript>().boxLines(rowButtons[(b+1).ToString() + i.ToString()]);
                    _box.GetComponent<BoxScript>().boxLines(colButtons[b.ToString() + i.ToString()]);
                    _box.GetComponent<BoxScript>().boxLines(colButtons[b.ToString() + (i + 1).ToString()]);
                    _box.tag = "unchecked";
                    
                    _box.name = (b + i).ToString();
                    boxes.Add(_box);
                }
              

            }
        }
        ggText.GetComponent<RectTransform>().SetAsLastSibling();
    
    }
    public void ButtonClicked(Button _b, bool _row){
        //Debug.Log(_b.name);
        bool alreadyClicked = false;
        if(_row && rowClicked[_b.name] != true){
            rowClicked[_b.name] = true;
        }else if(!_row && colClicked[_b.name] != true){
            colClicked[_b.name] = true;
        }else{
            alreadyClicked = true;
        }
        if(!alreadyClicked){
             if(firstPlayerTurn){
                _b.GetComponent<Image>().color = Color.blue;
            }else if(!firstPlayerTurn){
                _b.GetComponent<Image>().color = Color.red;
            }
        

            CheckBoxes();
        }
       
        
    }
    private bool didCheck = false;
    private void CheckBoxes(){
        bool[] clicked = new bool[4];
        bool pointGained = false;
        
        foreach(GameObject box in boxes ){
            if(box.tag == "unchecked"){
                List<Button> temp =  box.GetComponent<BoxScript>().buttonList;
                int counter = 0;
                foreach(Button but in temp){
                    //Debug.Log(but.name);
                    if(counter < 2){
                        if(rowClicked[but.name]){
                        clicked[counter] = true; 
                        }else{ clicked[counter] = false ;}
                    }else if (counter < 4){
                        if(colClicked[but.name]){
                            clicked[counter] = true; 
                        }else{ clicked[counter] = false;}

                    }else{
                        //counter >= 4
                    }   
                    counter ++;
                    
                }
                if(clicked[0] && clicked[1] && clicked[2] && clicked[3]){
                    box.GetComponent<BoxScript>().ButtonSurrounded(firstPlayerTurn);
                    pointGained = true;
                    if(firstPlayerTurn){
                        p1Score++;
                    }else if(!firstPlayerTurn){
                        p2Score++;
                    }
                    
                }
                
            }
        }
        if(!pointGained){
                Debug.Log("change turn");
                firstPlayerTurn = !firstPlayerTurn;
                
        }
        foreach(GameObject box in boxes ){
            if(box.tag == "unchecked"){
                didCheck = true;
            }
        }
        Debug.Log(didCheck);
        if(!didCheck && !gameover){
            gameover = true;
        }else{
            didCheck = false;
        }
        
    }
    
}
