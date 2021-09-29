using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class LakeManager : MonoBehaviour
{
    [SerializeField] private Transform playerSitOnIceTransform;
    [SerializeField] private Transform playerClimbTransform;
    [SerializeField] private Transform goalTransform;

    public UnityEvent onRogerFallsInWater;
    
    [SerializeField] private PlayerController _playerController;

    private void Start()
    {
        FindAndSetPlayer();
    }

    public void FindAndSetPlayer()
    {
        _playerController = PlayerController.Instance;
    }

    public void SetPlayerOnIce()
    {
        _playerController.transform.position = playerSitOnIceTransform.position;
        _playerController.transform.rotation = playerSitOnIceTransform.rotation;
        
        _playerController.DisableTeleporting();
    }

    public void RogerWillFallInWithDelay(float delay)
    {
        StartCoroutine(RogerWillFallInRoutine(delay));
    }

    private IEnumerator RogerWillFallInRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        
        onRogerFallsInWater.Invoke();
        
        SetPlayerClimbing();
    }

    private void SetPlayerClimbing()
    {
        _playerController.transform.position = playerClimbTransform.position;
        _playerController.transform.rotation = playerClimbTransform.rotation;
        
        _playerController.EnableClimbing();
    }

    public void PlayerGoal()
    {
        _playerController.DisableClimbing();
        _playerController.Kill();
        
        LoadLevel.Instance.StartLoadingScene("Menu");

        StartCoroutine(nameof(SetPlayerGoalPositionRoutine));
    }

    private IEnumerator SetPlayerGoalPositionRoutine()
    {
        yield return new WaitForSeconds(LoadLevel.Instance.TransitionDelatTime);
       
        _playerController.transform.position = goalTransform.position;
        _playerController.transform.rotation = goalTransform.rotation;
    }
}
