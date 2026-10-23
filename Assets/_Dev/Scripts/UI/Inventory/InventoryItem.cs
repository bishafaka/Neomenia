using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItem : DraggableUIElement
{
    [SerializeField] Image image;
    [HideInInspector] public Item item;
    [HideInInspector] public Transform parentOnEndDrag;
    public void InitItem(Item newItem)
    {
        item=newItem;
        image.sprite=newItem.icon;
    }
    public override void Awake()
    {
        base.Awake();
        if(image==null)
            image=GetComponent<Image>();
    }
    public override void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget=false;
        parentOnEndDrag=transform.parent;
        transform.SetParent(transform.root);
        base.OnBeginDrag(eventData);
    }
    public override void OnDrag(PointerEventData eventData)
    {
        base.OnDrag(eventData);
    }
    public override void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget=true;
        transform.SetParent(parentOnEndDrag);
        base.OnEndDrag(eventData);
    }
}
