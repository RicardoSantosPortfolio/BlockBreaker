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

using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static private GameManager PrivateInstance;
    static public GameManager Instance { get { return PrivateInstance; } }

    public bool gameStarted = false;
    public TextMeshProUGUI scoreDisplay;
    public GameObject Cog;
    public GameObject Paddle;
    public GameObject Background;
    public float backgroundShakeRate = 2.0f;

    private int gameScore = 0;
    
    

    private void Awake()
    {
        if (PrivateInstance != null && PrivateInstance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            PrivateInstance = this;
        }
    }

    public void IncreaseScore(int value)
    {
        gameScore += value;
        scoreDisplay.text = gameScore.ToString();

        LeanTween.cancel(scoreDisplay.gameObject);
        scoreDisplay.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        scoreDisplay.transform.localScale = Vector3.one;

        LeanTween.rotateZ(scoreDisplay.gameObject, 15.0f, 0.5f).setEasePunch();
        LeanTween.scaleX(scoreDisplay.gameObject, 1.5f, 0.5f).setEasePunch();

        LeanTween.move(Background.gameObject, Random.insideUnitCircle * backgroundShakeRate, 0.5f)
            .setEasePunch();

        Background.LeanColor(Color.red, 0.3f).setEasePunch().setOnComplete(() =>
        {
            Background.GetComponent<SpriteRenderer>().color = new Color(0.38f, 0.38f, 0.38f);
        });
    }

    public void ResetGame()
    {
        Cog.GetComponent<Rigidbody2D>().position = Vector2.zero;
        Cog.GetComponent<Rigidbody2D>().velocity = Vector2.down * BallScript.BallSpeed;
        Paddle.GetComponent<Rigidbody2D>().position = new Vector2(0, -4);

    }
}
