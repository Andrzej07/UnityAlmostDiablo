using UnityEngine;

namespace Demo.Abilities
{
    public class AnimationOnCast : MonoBehaviour, IPostCastAction
    {
        new public string animation;
        Animator animator;

        void Start()
        {
            Ability ability = GetComponent<Ability>();
            animator = ability.source.GetComponentInChildren<Animator>();
        }

        public void Perform()
        {
            animator.Play(animation);
        }
    }
}