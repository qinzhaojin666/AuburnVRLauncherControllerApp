using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlackBot : MonoBehaviour
{
    public string message;
    public GameObject buttonObject;

    Button button;
    private bool canSend = true;

    private void Start()
    {
        button = buttonObject.GetComponent<Button>();
        button.onClick.AddListener(delegate { Send(message); });
    }

    private void Update()
    {
       
    }

    /// <summary>
    /// Sends a message to a Slack channel using the Slackbot API through the chat.postMessage (https://api.slack.com/methods/chat.postMessage)
    /// </summary>
    private void Send(string message)
    {
        if (canSend = true)
        {
            canSend = false;
            Debug.Log("Sending message '{0}'..." + message);

            var url = "https://slack.com/api/chat.postMessage";
            var data = new WWWForm();

            // Create your Slackbot and its API token here: https://my.slack.com/services/new/bot
            data.AddField("token", "xoxb-732013548983-718704273906-XWdxMuwM1hLJh0xBjSqhVOxI");

            // The Slack channel.
            data.AddField("channel", "webhook-pushes");

            // How the Slack bot will be identified in Slack.
            data.AddField("username", "Botty McBotFace");

            data.AddField("text", message);

            var post = new WWW(url, data);
            StartCoroutine(WaitForPost(post));
        }
    }

    private IEnumerator WaitForPost(WWW post)
    {
        yield return post;

        Debug.Log("Message sent.");

        yield return new WaitForSeconds(1);

        canSend = true;
    }
}
