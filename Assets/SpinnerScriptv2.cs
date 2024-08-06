using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//random thời gian + điểm -> tốc độ ban đầu
public class SpinnerScriptv2 : MonoBehaviour
{
    public Button spinButton; // btn quay
    public TextMeshProUGUI point; // kết quả
    public TextMeshProUGUI finalPoint; // kết quả hiển thị trước

    private int pointIndex;
    private int numberOfSpins = 5; // số lần quay tối thiểu

    private float spinTime; // thời gian quay
    private float currentSpeed; // tốc độ quay hiện tại (/s)

    private float speedDecrease; // tốc độ giảm dần theo thời gian
    private bool isSpinning = false; // trạng thái vòng quay
    private int[] points = new int[] { 100, 200, 300, 400, 500, 600 }; // điểm

    //Sự kiện click btn quay
    public void OnSpinButtonClick()
    {
        spinTime = Random.Range(4, 6); // random thời gian
        pointIndex = Random.Range(0, 5); // random điểm
        float rotation = Random.Range(pointIndex * 60,(pointIndex + 1 )* 60); // random góc quay dựa trên điểm vd: 100 điểm -> index = 0 -> 0 - 60


        //tổng góc quay + góc quay lần quay trước đó = góc quay mục tiêu + 360 * số lần quay tối thiểu
        float totalRotation = rotation + 360 * numberOfSpins - transform.eulerAngles.z; // tính góc quay vòng quay sẽ quay được


        //công thức s = 1/2 * v * t -> v = 2 * s / t (v dừng = 0)
        currentSpeed = totalRotation * 2 / spinTime; // tính tốc độ ban đầu

        speedDecrease = currentSpeed / spinTime; // Tính gia tốc giảm dần
        isSpinning = true;
        point.text = "Spinning...";
        spinButton.interactable = false;


        finalPoint.text = points[pointIndex].ToString(); // hiển thị kết quả
    }

    void Update()
    {
        //Kiểm tra vòng quay có đang quay ko
        if (!isSpinning)
        {
            return;
        }
        //quay
        transform.Rotate(Vector3.forward, currentSpeed * Time.deltaTime);
        //tốc độ giảm dần
        if (currentSpeed > 0)
        {
            currentSpeed -= speedDecrease * Time.deltaTime;
        }
        //kiểm tra điều kiện dừng
        if (currentSpeed <= 0)
        {
            isSpinning = false;
            spinButton.interactable = true;
            float angleZ = transform.eulerAngles.z;
            int pointIndex = Mathf.FloorToInt(angleZ / 60); // Tính số góc đã quay được

            point.text = points[pointIndex].ToString(); // hiển thị kết quả
        }
    }
}
