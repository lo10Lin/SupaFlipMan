using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreenControl : MonoBehaviour
{
    private Animator animator;
    public Animator transition;
    private float timePressInput;
    private readonly float shortCooldown = 0.3f;
    private readonly float longColdown = 0.7f;
    private readonly float lowValue = 0.2f;
    private readonly float highValue = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        timePressInput = Time.time + 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 stickDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (stickDir.x > highValue && stickDir.y < lowValue && stickDir.y > -lowValue && timePressInput <= Time.time)
        {
            animator.SetTrigger("Switch");
            timePressInput = Time.time + shortCooldown;
        }

        else if (stickDir.x < -highValue && stickDir.y < lowValue && stickDir.y > -lowValue && timePressInput <= Time.time)
        {
            animator.SetTrigger("Switch");
            timePressInput = Time.time + shortCooldown;
        }

        else if (Input.GetButtonDown("Fire1") && timePressInput <= Time.time)
        {
            CheckButtonA();
        }

    }

    private void CheckButtonA()
    {
        AnimatorClipInfo[] currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);
        string name = currentClipInfo[0].clip.name;

        if (name[0] == 'R')
        {
            StartCoroutine(Transition(SceneManager.GetActiveScene().buildIndex));
        }
        else if (name[0] == 'M')
        {
            StartCoroutine(Transition(0));
        }
    }

    private IEnumerator Transition(int sceneId)
    {
        enabled = false;
        transition.SetTrigger("Load");
        yield return new WaitForSecondsRealtime(1.1f);
        SceneManager.LoadScene(sceneId);
    }
}
