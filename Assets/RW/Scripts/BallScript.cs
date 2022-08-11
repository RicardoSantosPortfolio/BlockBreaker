/*
 * Copyright (c) 2021 Razeware LLC
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * Notwithstanding the foregoing, you may not use, copy, modify, merge, publish, 
 * distribute, sublicense, create a derivative work, and/or sell copies of the 
 * Software in any work that is designed, intended, or marketed for pedagogical or 
 * instructional purposes related to programming, coding, application development, 
 * or information technology.  Permission for such use, copying, modification,
 * merger, publication, distribution, sublicensing, creation of derivative works, 
 * or sale is expressly withheld.
 *    
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;

public class BallScript : MonoBehaviour
{
    static public float BallSpeed = 5.0f;

    public AnimationCurve animationCurve;

    private float hitPosition = 0;
    private Vector2 direction;
    private Rigidbody2D ballRigidbody;

    private void Awake()
    {
        ballRigidbody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ballRigidbody.velocity = Vector2.down * BallSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            hitPosition = (ballRigidbody.position.x - collision.rigidbody.position.x) 
                / collision.collider.bounds.size.x;

            direction = new Vector2(hitPosition, 1).normalized;
            ballRigidbody.velocity = direction * BallSpeed;
        }

        LeanTween.cancel(gameObject);
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);

        LeanTween.scale(gameObject, new Vector3(1.0f, 1.0f), 1.0f).setEase(animationCurve);

        gameObject.LeanColor(Color.yellow, 0.5f).setEasePunch();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.ResetGame();
    }
}
