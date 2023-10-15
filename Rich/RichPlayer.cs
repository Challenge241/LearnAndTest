using System.Collections;
using UnityEngine;
using UnityEngine.UI;


// ����һ��������ҿ��ƺ���Ϊ����
public class RichPlayer : MonoBehaviour
{
    public Transform[] tiles; // �������洢����Ϸ�������еĸ��ӵ�Transform
    public int currentTileIndex = 0; // ���������¼����ҵ�ǰ���ڵĸ����������е�����
    public float speed = 2f; // ���������������ҵ��ƶ��ٶ�
    public int diceValue;
    public int money; // ��ҵ�Ǯ
    public int DianJuanNum=100;
    public GameObject buyLandDialog; // �������صĶԻ���
    public GameObject GO;
    public GameObject rollingAni;
    public GameObject[] rollResults;
    public UnityEngine.UI.Text feedbackText;
    public GameObject textBG;
    public UnityEngine.UI.Text LandmoneyText;
    public string RicherName;
    public GameObject buildHouseDialog;
    public GameObject upgradeHouseDialog;
    void Start()
    {
        GameObject Roads = GameObject.Find("Roads"); // �ҵ���Ϊ "Roads" ������
        tiles = new Transform[Roads.transform.childCount]; // ��ʼ�� tiles ����

        for (int i = 0; i < Roads.transform.childCount; i++)
        {
            tiles[i] = Roads.transform.GetChild(i); // ��ȡÿ��������� Transform�������� tiles ����
        }
    }


    // �����Ͷ������ʱ�����������
    public void ThrowDice()
    {
        diceValue = Random.Range(1, 7); // ����һ��1��6����������������ӵĽ��
        //print(diceValue);
        rollingAni.GetComponent<SpriteRenderer>().enabled=false;
        rollResults[diceValue - 1].SetActive(true);
        MovePlayer(diceValue); // �������ӵĽ���ƶ����
    }

    // �����������ָ���ĸ������ƶ����
    private void MovePlayer(int numTiles)
    {
        // ����Ŀ����ӵ����������Ŀ����ӳ����˸�������ĳ��ȣ���ص���һ��
        int targetTileIndex = (currentTileIndex + numTiles) % tiles.Length;
        // ��ʼһ��Э�̣�������ƶ���Ŀ�����
        StartCoroutine(MoveToTargetTile(targetTileIndex));
    }

    // ����һ��Э�̣������ƶ���ҵ�Ŀ�����

    private IEnumerator MoveToTargetTile(int targetTileIndex)
    {
        GO.SetActive(false);
        // ����һ�û�е���Ŀ�����ʱ�������ƶ����
        while (currentTileIndex != targetTileIndex)
        {
            // ����ǰ����������һ����ʾ���Ҫ�ƶ�����һ������
            currentTileIndex = (currentTileIndex + 1) % tiles.Length;
            // ��ȡĿ����ӵ�λ��
            Vector3 targetPosition = tiles[currentTileIndex].position;
            // ����һ�û�е���Ŀ�����ʱ��ÿһ֡����Ŀ������ƶ�һ���ľ���
            while (transform.position != targetPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                yield return null; // �ȴ���һ֡
            }
        }

        richRoad tile = tiles[currentTileIndex].GetComponent<richRoad>();

        if (tile.land != null)
        {
            if (tile.land.owner == null)
            {
                // �����������������أ���������û�б�������ʾ�������صĶԻ���
                buyLandDialog.SetActive(true);
                LandmoneyText.text = "BuyThisLandNeed" + tile.land.price;
            }
            else if (tile.land.owner != this)
            {
                // �����������������أ��������ر����˹�������˽���·��
                int tollFee = CalculateTollFee(tile.land); // CalculateTollFee������Ҫ���Լ�ʵ�֣������·�ѵĽ��
                if (money >= tollFee)
                {
                    money -= tollFee;
                    textBG.SetActive(true);
                    feedbackText.text = RicherName + "Pay" + tollFee + "RunFee"; // �޸��ı����е��ı�
                    tile.land.owner.money += tollFee;
                    ButtonManager.instance.moneyText.text=money.ToString();
                    yield return new WaitForSeconds(1.0f);
                    textBG.SetActive(false);

                    turnEnd();
                }
                else
                {
                    textBG.SetActive(true);
                    feedbackText.text = RicherName + "have not enough money to pay for tolls"; // �޸��ı����е��ı�
                    // �����ҵ�Ǯ����֧����·�ѣ��������Ҫ��һЩ���������Ʋ���������һЩ����
                    // ����߼���Ҫ����������Ϸ������ʵ��
                    yield return new WaitForSeconds(2.0f);
                    textBG.SetActive(false);

                    turnEnd();
                }
            }
            else
            {//�ߵ����Լ��ĵؿ���

                if (tile.land.house == null)
                {
                    // �������ؿ���û�з��ӣ���ʾ���췿�ӵĶԻ���
                    buildHouseDialog.SetActive(true);
                }
                else if (tile.land.level < tile.land.maxLevel)
                {
                    upgradeHouseDialog.SetActive(true);
                    // �������ֲ����Ӷ���moneyText����ֵ
                    Transform moneyTextTransform = upgradeHouseDialog.transform.Find("MoneyText");
                    int a=tile.land.type;
                    // ���ݷ��ݵ����ͣ���moneyText�Ӷ�����ı���ֵ
                    Text moneyText = moneyTextTransform.GetComponent<Text>();
                    if (moneyText != null)
                    {
                        moneyText.text = tile.land.buildCost[a].ToString();
                    }
                }
                else
                {
                    turnEnd();
                }
                
            }
        }
        else if(tile.TitleDianJuan!=0)
        {
            //print(tile.TitleDianJuan);
            DianJuanNum = DianJuanNum +tile.TitleDianJuan;
            ButtonManager.instance.DianJuanText.text = DianJuanNum.ToString();
            yield return new WaitForSeconds(1.0f);
            turnEnd();
        }else if (tile.qiQiu!=null)
        {
            //print("tile.qiQiu");
            tile.qiQiu.GameStart(this);
            turnEnd();
        }
    }
    private int CalculateTollFee(RichLand land)
    {
        if (land.type == -1)
        {
            return (int)(land.price * 0.15f); // Calculate 15% of the land's price
        }
        else if (land.type == 0)
        {
            return (int)(land.price *land.level* 1.5f); // Calculate 150% of the land's price
        }
        else if (land.type == 1)
        {
            return land.price * land.level; // Return the land's price
        }
        else if (land.type == 2)
        {
            return (int)(land.price * land.level * 0.5f); // Calculate 50% of the land's price
        }
        else
        {
            throw new System.ArgumentException("Invalid land type.");
        }
    }



    public void BuyLand()
    {
        // ���ѡ���˹�������
        richRoad tile = tiles[currentTileIndex].GetComponent<richRoad>();
        if (money >= tile.land.price)
        {
            // �����ҵ�Ǯ�㹻�������������
            tile.land.owner = this;
            money -= tile.land.price;

            // ��ȡ�����ϵ�SpriteRenderer������޸���ɫΪ�����ɫ
            SpriteRenderer landSpriteRenderer = tile.land.GetComponent<SpriteRenderer>();
            if (landSpriteRenderer != null)
            {
                SpriteRenderer playerSpriteRenderer = this.GetComponent<SpriteRenderer>();
                if (playerSpriteRenderer != null)
                {
                    landSpriteRenderer.color = playerSpriteRenderer.color;
                }
            }
        }
        else
        {
            //feedbackText.text = "Ǯ����";
        }
        // �رչ������صĶԻ���
        buyLandDialog.SetActive(false);

        turnEnd();
    }


    public void DeclineBuyLand()
    {
        // ���ѡ���˲��������أ��رչ������صĶԻ���,�����ж���
        buyLandDialog.SetActive(false);

        turnEnd();
    }
    private void turnEnd()//һ�ֽ������µ�һ�ֿ�ʼ
    {
        GO.SetActive(true);
        rollingAni.GetComponent<SpriteRenderer>().enabled = true;
        rollResults[diceValue - 1].SetActive(false);
        ButtonManager.instance.currentPlayerIndex = (ButtonManager.instance.currentPlayerIndex + 1) % ButtonManager.instance.players.Count;
        ButtonManager.instance.nameText.text = ButtonManager.instance.players[ButtonManager.instance.currentPlayerIndex].RicherName;
        ButtonManager.instance.moneyText.text = ButtonManager.instance.players[ButtonManager.instance.currentPlayerIndex].money.ToString();
        ButtonManager.instance.DianJuanText.text = ButtonManager.instance.players[ButtonManager.instance.currentPlayerIndex].DianJuanNum.ToString();
        // ��������ͷ��Ŀ��Ϊ��ǰ���
        ButtonManager.instance.cameraF.target = ButtonManager.instance.players[ButtonManager.instance.currentPlayerIndex].transform;
    }
    public void BuildHouse(int i)
    {
        // ��ȡ��ǰ�ؿ����Ϣ
        richRoad tile = tiles[currentTileIndex].GetComponent<richRoad>();
        //print("PlayerBuildHouse" + i);
        // �������Ƿ����㹻��Ǯ�����췿��
        if (money >= tile.land.buildCost[i])
        {
            // �۳����췿�ӵķ���
            money -= tile.land.buildCost[i];

            // �ڵؿ��Ͻ��췿��
            tile.land.BuildHouse(i);

            // ����UI��ʾ
            //HousemoneyText.text = "�Ѿ����췿�ӡ�ʣ���Ǯ��" + money;
        }
        else
        {
            // ������û���㹻��Ǯ����ʾһ����Ϣ
            //HousemoneyText.text = "��û���㹻��Ǯ�����췿�ӡ�";
        }

        // �رս��췿�ӵĶԻ���
        buildHouseDialog.SetActive(false);
        turnEnd();
    }
    public void DeclineBuildHouse()
    {
        buildHouseDialog.SetActive(false);
        turnEnd();
    }
    public void UpgradeHouse()
    {
        richRoad tile = tiles[currentTileIndex].GetComponent<richRoad>();
        int a = tile.land.type;
        if (money >= tile.land.buildCost[a])
        {
            money -= tile.land.buildCost[a];
            tile.land.UpgradeHouse(a);
        }
        else
        {
            
        }
        upgradeHouseDialog.SetActive(false);
        turnEnd();
    }
    public void DeclineUpgradeHouse()
    {
        upgradeHouseDialog.SetActive(false);
        turnEnd();
    }
}
