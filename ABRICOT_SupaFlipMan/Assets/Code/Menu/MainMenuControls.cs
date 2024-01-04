using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour
{
    private Animator animator;
    public Animator transition;
    private bool canMoveVerticaly = false;
    private bool creditMenu = false;
    private float timePressInput;
    private readonly float shortCooldown = 0.3f;
    private readonly float longColdown = 0.7f;

    private readonly float lowValue = 0.2f;
    private readonly float highValue = 0.9f;
    public bool isFirstTimePlay = true;
    void Start()
    {
        animator = GetComponent<Animator>();
        timePressInput = Time.time + 2;
        AudioManager.audioInstance.isFistTimePlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFirstTimePlay)
        {
            AudioManager.audioInstance.StopSound(67);
            AudioManager.audioInstance.PlaySound(66);
            isFirstTimePlay = false;
        }
        if (!creditMenu)
        {
            Vector2 stickDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

            if(stickDir.x > highValue && stickDir.y < lowValue && stickDir.y > -lowValue && timePressInput <= Time.time)
            {
                AudioManager.audioInstance.PlaySound(55);
                animator.SetTrigger("Right");
                timePressInput = Time.time + shortCooldown;
            }

            else if (stickDir.x < -highValue && stickDir.y < lowValue && stickDir.y > -lowValue && timePressInput <= Time.time)
            {
                AudioManager.audioInstance.PlaySound(55);
                animator.SetTrigger("Left");
                timePressInput = Time.time + shortCooldown;
            }

            else if (Input.GetButtonDown("Fire1") && timePressInput <= Time.time)
            {
                AudioManager.audioInstance.PlaySound(54);
                CheckButtonA();
            }

            else if (Input.GetButtonDown("Fire2") && timePressInput <= Time.time)
            {
                CheckButtonB();
            }

            else if (canMoveVerticaly)
            {
                if (stickDir.y > highValue && stickDir.x < lowValue && stickDir.x > -lowValue && timePressInput <= Time.time)
                {
                    AudioManager.audioInstance.PlaySound(55);
                    animator.SetTrigger("Up");
                    timePressInput = Time.time + 0.2f;
                }
                else if (stickDir.y < -highValue && stickDir.x < lowValue && stickDir.x > -lowValue && timePressInput <= Time.time)
                {
                    AudioManager.audioInstance.PlaySound(55);
                    animator.SetTrigger("Down");
                    timePressInput = Time.time + 0.2f;
                }
            }
        }
        else if(Input.GetButtonDown("Fire2") && timePressInput <= Time.time)
        {
            creditMenu = false;
            AudioManager.audioInstance.PlaySound(56);
            animator.SetTrigger("B");
            timePressInput = Time.time + longColdown;
        }
    }

    private void CheckButtonA()
    {
        AnimatorClipInfo[] currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        string name = currentClipInfo[0].clip.name;

        if (name[0] == 'L' && name[name.Length - 1] - '0' < 6)
        {
            StartCoroutine(Transition(name[name.Length - 1] - '0'));
            enabled = false;
        }
        else if (name[0] == 'P')
        {
            canMoveVerticaly = true;
            animator.SetTrigger("A");
            timePressInput = Time.time + 1.5f;
        }
        else if (name[0] == 'C')
        {
            creditMenu = true;
            animator.SetTrigger("A");
            timePressInput = Time.time + longColdown;
        }
        else if (name[0] == 'Q')
        {
            print("Quit Game");
            Application.Quit();
        }
    }

    private void CheckButtonB()
    {
        AnimatorClipInfo[] currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        string name = currentClipInfo[0].clip.name;

        if (name[0] - 'P' != 0 && (name[0] - 'C' != 0 || name[name.Length - 1] - 'u' == 0) && name[0] - 'Q' != 0)
        {
            canMoveVerticaly = false;
            AudioManager.audioInstance.PlaySound(56);
            animator.SetTrigger("B");
            timePressInput = Time.time + longColdown;
        }
    }

    private IEnumerator Transition(int sceneId)
    {
        transition.SetTrigger("Load");
        yield return new WaitForSecondsRealtime(1.1f);
        SceneManager.LoadScene(sceneId);
    }
}
