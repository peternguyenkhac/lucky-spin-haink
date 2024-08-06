using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//random thời gian + random tốc độ ban đầu (opt) -> tính ra điểm
public class SpinnerScript : MonoBehaviour
{
    public Button spinButton; // btn quay
    public TextMeshProUGUI point; // kết quả
    public TextMeshProUGUI finalPoint; // kết quả hiển thị trước

    private float spinSpeed = 500f; // tốc độ quay ban đầu
    private float spinTime; // thời gian quay
    private float currentSpeed; // tốc độ quay hiện tại (/s)
    private float speedDecrease; // tốc độ giảm dần theo thời gian
    private bool isSpinning = false; // trạng thái vòng quay
    private int[] points = new int[] { 100, 200, 300, 400, 500, 600 }; // điểm

    //Sự kiện click btn quay
    public void OnSpinButtonClick()
    {
        spinTime = Random.Range(4, 6);
        currentSpeed = spinSpeed;
        speedDecrease = spinSpeed / spinTime; // Tính gia tốc giảm dần
        isSpinning = true;
        point.text = "Spinning...";
        spinButton.interactable = false;


        //công thức tính tổng số góc quay (quãng đường) dựa trên gia tốc giảm dần = 1/2 * v * t (vận tốc về 0)
        double result = 0.5 * spinSpeed * spinTime;


        //tổng góc quay + góc quay lần quay trước đó = góc quay mục tiêu + 360 * số lần quay tối thiểu
        //góc quay mục tiêu = (tổng góc quay + góc quay lần quay trước đó) % 360 
        double rotationDegrees = (result + transform.eulerAngles.z) % 360; // Tính số góc sẽ quay được (bao gồm cả góc trước đó đã quay)
        int pointIndex = Mathf.FloorToInt((float)rotationDegrees / 60); //Tính điẻm dựa trên góc quay

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
