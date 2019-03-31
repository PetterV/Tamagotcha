using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorModes : MonoBehaviour
{
    public bool cleanMode = false;
    public bool slapMode = false;
    public bool publicAppearanceMode = false;
    public bool politicsMode = false;

    public Texture2D cleanCursor;
    public Texture2D cleanCursorDown;
    public Texture2D slapCursor;
    public Texture2D slapCursorDown;
    public Texture2D publicAppearanceCursor;
    public Texture2D publicAppearanceCursorDown;
    public Texture2D politicsCursor;
    public Texture2D politicsCursorDown;

    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    InteractionSounds interactionSounds;

    float cursorTimer = 0;
    bool cursorDown = false;
    public float cursorDuration = 0.25f;

    void Start()
    {
        interactionSounds = GameObject.Find("Main Camera").GetComponent<InteractionSounds>();
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            DisableAllModes();
        }
        if (cursorDown)
        {
            cursorTimer += Time.deltaTime;
            if (cursorTimer >= cursorDuration)
            {
                cursorDown = false;

                if (cleanMode)
                {
                    Cursor.SetCursor(cleanCursor, hotSpot, cursorMode);
                }
                else if (slapMode)
                {
                    Cursor.SetCursor(slapCursor, hotSpot, cursorMode);
                }
                else if (publicAppearanceMode)
                {
                    Cursor.SetCursor(publicAppearanceCursor, hotSpot, cursorMode);
                }
                else if (politicsMode)
                {
                    Cursor.SetCursor(politicsCursor, hotSpot, cursorMode);
                }
                else
                {
                    Cursor.SetCursor(null, hotSpot, cursorMode);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (cleanMode)
            {
                Cursor.SetCursor(cleanCursorDown, hotSpot, cursorMode);
                cursorDown = true;
                cursorTimer = 0f;
            }
            else if (slapMode)
            {
                Cursor.SetCursor(slapCursorDown, hotSpot, cursorMode);
                cursorDown = true;
                cursorTimer = 0f;
            }
            else if (publicAppearanceMode)
            {
                Cursor.SetCursor(publicAppearanceCursorDown, hotSpot, cursorMode);
                cursorDown = true;
                cursorTimer = 0f;
            }
            else if (politicsMode)
            {
                Cursor.SetCursor(politicsCursorDown, hotSpot, cursorMode);
                cursorDown = true;
                cursorTimer = 0f;
            }
            else
            {
                Cursor.SetCursor(null, hotSpot, cursorMode);
            }
        }
    }
    void DisableAllModes()
    {
        cleanMode = false;
        slapMode = false;
        publicAppearanceMode = false;
        politicsMode = false;
        Cursor.SetCursor(null, hotSpot, cursorMode);
        cursorDown = false;
    }

    public void EnableCleanMode()
    {
        DisableAllModes();
        cleanMode = true;
        Cursor.SetCursor(cleanCursor, hotSpot, cursorMode);
        cursorDown = false;
        interactionSounds.PlayEmptyClick();
    }

    public void EnableSlapMode()
    {
        DisableAllModes();
        slapMode = true;
        Cursor.SetCursor(slapCursor, hotSpot, cursorMode);
        cursorDown = false;
        interactionSounds.PlayEmptyClick();
    }

    public void EnablePublicAppearanceMode()
    {
        DisableAllModes();
        publicAppearanceMode = true;
        Cursor.SetCursor(publicAppearanceCursor, hotSpot, cursorMode);
        cursorDown = false;
        interactionSounds.PlayEmptyClick();
    }

    public void EnablePoliticsMode()
    {
        DisableAllModes();
        politicsMode = true;
        cursorDown = false;
        Cursor.SetCursor(politicsCursor, hotSpot, cursorMode);
        interactionSounds.PlayEmptyClick();
    }
}
