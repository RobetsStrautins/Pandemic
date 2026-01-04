using UnityEngine;
using UnityEngine.UI;

public class RolesDescription : MonoBehaviour
{
    public static RolesDescription Instance;

    public Text title;
    public Text description;
    public GameObject panel;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    public void showRoleDescription(Player player)
    {
        panel.SetActive(true);
        title.text = player.playerRole.ToString();

        switch (player.playerRole)
        {
            case PlayerRole.Medic:
                description.text =
                    "Ārstējot slimību, noņem VISUS vienas krāsas slimību kubus no pilsētas. " +
                    "Ja slimība ir izārstēta, ienākot pilsētā, visi šīs krāsas kubi tiek noņemti automātiski.";
                break;

            case PlayerRole.Scientist:
                description.text =
                    "Var izārstēt slimību, izmantojot TIKAI 4 vienas krāsas pilsētu kārtis " +
                    "(citiem spēlētājiem nepieciešamas 5 kārtis).";
                break;

            case PlayerRole.QuarantineSpecialist:
                description.text =
                    "Novērš slimību kubu izvietošanu savā pilsētā un visās tieši savienotajās pilsētās. " +
                    "Tas attiecas arī uz uzliesmojumiem.";
                break;

            case PlayerRole.Researcher:
                description.text =
                    "Var nodot jebkuru pilsētas kārti citam spēlētājam, ja abi atrodas vienā pilsētā. " +
                    "Nav jānodod tikai tās pilsētas kārts, kurā atrodas.";
                break;

            case PlayerRole.OperationsExpert:
                description.text =
                    "Var uzbūvēt pētniecības staciju bez pilsētas kārts atmešanas. " +
                    "No pilsētas ar staciju var pārvietoties uz jebkuru citu pilsētu, atmetot vienu kārti.";
                break;

            case PlayerRole.ContingencyPlanner:
                description.text =
                    "Var paņemt vienu notikuma kārti no atmetamo kāršu kaudzes un glabāt to. " +
                    "Šo kārti var izmantot jebkurā brīdī, pēc tam tā tiek izņemta no spēles.";
                break;
                
            case PlayerRole.Dispatcher:
                description.text =
                    "Var pārvietot citus spēlētājus, tērējot savas darbības. " +
                    "Ja atrodas vienā pilsētā ar citu spēlētāju, var pārvietot viņu uz jebkuru pilsētu.";
                break;
        }
    }

    public void hide()
    {
        panel.SetActive(false);
    }
}
