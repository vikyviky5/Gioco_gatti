using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayFabAuthManager : MonoBehaviour
{
    [Header("TUTTI I TUOI PANNELLI (Trascinali qui)")]
    public GameObject pannelloIntroLogo;    
    public GameObject bottoneAvvia;
    public GameObject sfondoBottoni;    
    public GameObject pannelloScelta;
    public GameObject pannelloLogin;
    public GameObject pannelloScelta2;
    public GameObject pannelloEsci;
    public GameObject pannelloRegistrazione;
    public GameObject pannelloResetPassword;
    public GameObject resetPassword;
    public GameObject pannelloPassword;

    [Header("CAMPI DI TESTO LOGIN (Trascinali qui)")]
    public TMP_InputField loginNomeUtente;
    public TMP_InputField loginPassword;

    [Header("CAMPI DI TESTO REGISTRAZIONE (Trascinali qui)")]
    public TMP_InputField regNomeUtente;
    public TMP_InputField regEmail;
    public TMP_InputField regPassword;

    [Header("CAMPO EMAIL PER RESET PASSWORD (Trascinalo qui)")]
    public TMP_InputField resetEmail;


    private void Start()
    {
        DisattivaTutto(); 
    
        if(pannelloIntroLogo) pannelloIntroLogo.SetActive(true);
        if(bottoneAvvia) bottoneAvvia.SetActive(true);
        
        // Assicuriamoci che lo sfondo dei bottoni sia spento all'inizio
        if(sfondoBottoni) sfondoBottoni.SetActive(false);
    }

    private void DisattivaTutto()
    {
        if(pannelloScelta) pannelloScelta.SetActive(false);
        if(pannelloLogin) pannelloLogin.SetActive(false);
        if(pannelloScelta2) pannelloScelta2.SetActive(false);
        if(pannelloEsci) pannelloEsci.SetActive(false);
        if(pannelloRegistrazione) pannelloRegistrazione.SetActive(false);
        if(pannelloResetPassword) pannelloResetPassword.SetActive(false);
        if(resetPassword) resetPassword.SetActive(false);
        if(pannelloPassword) pannelloPassword.SetActive(false);
    }

    public void ClicSuAvvia(){
        if(pannelloIntroLogo) pannelloIntroLogo.SetActive(false);
        if(bottoneAvvia) bottoneAvvia.SetActive(false);

        // ACCENDIAMO lo sfondo qui:
        if(sfondoBottoni) sfondoBottoni.SetActive(true);

        ApriSceltaIniziale();
    }

    // Metti questa sul bottone "INDIETRO" o all'avvio del gioco
    public void ApriSceltaIniziale() 
    {
        DisattivaTutto();
        pannelloScelta.SetActive(true);
        pannelloScelta2.SetActive(true);
    }

    // Metti questa sul bottone "ACCEDI" del menu iniziale
    public void ApriLogin() 
    {
        DisattivaTutto();
        pannelloLogin.SetActive(true);
        if(pannelloPassword) pannelloPassword.SetActive(true);
    }

    // Metti questa sul bottone "CREA ACCOUNT"
    public void ApriRegistrazione() 
    {
        DisattivaTutto();
        pannelloRegistrazione.SetActive(true);
    }

    // Metti questa sul bottone "Hai dimenticato la password?"
    public void ApriResetPassword() 
    {
        DisattivaTutto();
        pannelloResetPassword.SetActive(true);
    }


    // Metti questa sul bottone "CONFERMA" dentro il pannello Login
    public void BottoneEseguiLogin() 
    {
        Debug.Log("STO INVIANDO -> Utente: [" + loginNomeUtente.text + "] - Password: [" + loginPassword.text + "]");
        var request = new LoginWithPlayFabRequest {
            Username = loginNomeUtente.text,
            Password = loginPassword.text
        };
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
    }

    // Metti questa sul bottone "CONFERMA" dentro il pannello Registrazione
    public void BottoneEseguiRegistrazione() 
    {
        Debug.Log("STO INVIANDO -> Utente: [" + loginNomeUtente.text + "] - Password: [" + loginPassword.text + "]");
        var request = new RegisterPlayFabUserRequest {
            Username = regNomeUtente.text,
            Email = regEmail.text,
            Password = regPassword.text,
            RequireBothUsernameAndEmail = true
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    // Metti questa sul bottone "INVIA" dentro il pannello Reset Password
    public void BottoneRecuperaPassword()
    {
        var request = new SendAccountRecoveryEmailRequest {
            Email = resetEmail.text,
            TitleId = PlayFabSettings.TitleId
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnResetSuccess, OnError);
    }

    public void BottoneLogOut()
    {
        
        PlayFabClientAPI.ForgetAllCredentials();

        
        loginNomeUtente.text = "";
        loginPassword.text = "";

        ApriSceltaIniziale();

        Debug.Log("Utente disconnesso con successo.");
    }

   
    void OnLoginSuccess(LoginResult result) 
    {
        Debug.Log("Successo! L'utente è entrato.");
        // SceneManager.LoadScene("NomeTuaScenaGioco"); // Togli le sbarrette verdi quando avrai la scena del gioco
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result) 
    {
        Debug.Log("Account creato! Ti rimando al login.");
        ApriLogin(); // Rimanda l'utente al pannello di login
    }

    void OnResetSuccess(SendAccountRecoveryEmailResult result)
    {
        Debug.Log("Email di recupero inviata! Controlla la posta.");
        resetEmail.text = ""; // Pulisce il campo di testo
        ApriSceltaIniziale(); // Ti rimanda ai bottoni iniziali
    }

    void OnError(PlayFabError error) 
    {
        Debug.LogError("ERRORE: " + error.GenerateErrorReport());
    }
}