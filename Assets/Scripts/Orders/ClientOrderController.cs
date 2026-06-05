using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zeke.UI;

public class ClientOrderController : MonoBehaviour
{
    [SerializeField] private Client client;

    [Space]

    [SerializeField] private UIWindow orderWindow;
    [SerializeField] private UIWindow potionWindow;
    [SerializeField] private UIWindow timerWindow;

    [Space]

    [Tooltip("Not random yet, should be a list of orders, that get updated according to current difficulty")]
    [SerializeField] private Order order;
    [SerializeField] private Vector3 windowOffset;

    private UIWindow windowInstance;
    private Image timerImageInstance;

    private void Awake()
    {
        client.SetOrder(order);
    }

    private void Start()
    {
        CreateOrderWindow();
    }

    private void Update()
    {
        timerImageInstance.fillAmount = 1f - client.PatiencePercentage;
    }

    private void LateUpdate()
    {
        if (windowInstance != null)
        {
            windowInstance.transform.position = transform.position + windowOffset;
        }
    }

    private void CreateOrderWindow()
    {
        windowInstance = Instantiate(orderWindow, GameManager.WorldCanvas.transform);

        Transform root = windowInstance.TryGetElement<LayoutGroup>("Layout Group").transform;

        for (int i = 0; i < order.Recipes.Count; i++)
        {
            UIWindow newPotionWindow = Instantiate(potionWindow, root);
            //set sprite of order.Recipes[i] to window
        }

        timerImageInstance = Instantiate(timerWindow, root).TryGetElement<Image>("Image");
    }
}