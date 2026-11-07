using UnityEngine;

public class scooter : MonoBehaviour
{
    [Header("Di chuyển")]
    [SerializeField] private float tocDoXe = 15f;
    [SerializeField] private float heSoTangToc = 1.6f;
    [SerializeField] private float lucReXe = 50f;
    [SerializeField] private float giaToc = 5f;

    [Header("Phanh & Lùi")]
    [SerializeField] private float doGatPhanh = 5f;
    [SerializeField] private GameObject hieuUngPhanh;

    [Header("Nhảy")]
    [SerializeField] private float lucNhay = 8f;
    [SerializeField] private float thoiGianCoyote = 0.15f;
    [SerializeField] private float thoiGianDemNhay = 0.15f;
    [SerializeField] private bool hienLogDebug = false;

    private bool xeDangBat = false;
    private bool dangPhanh = false;
    private bool dangTangToc = false;
    private bool dangLui = false; // Thêm biến nhận diện đang lùi
    private float dauVaoRe;
    private float tocDoDangChay = 0f;

    private float thoiDiemChamDat = -10f;
    private float thoiDiemBamNhay = -10f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        if (hieuUngPhanh != null)
        {
            hieuUngPhanh.SetActive(false);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.GetContact(i).normal.y > 0.5f)
            {
                thoiDiemChamDat = Time.time;
                return;
            }
        }
    }

    private void Update()
    {
        // Mũi tên lên hoặc W: tiến tới + tăng tốc
        dangTangToc = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
        if (dangTangToc)
        {
            xeDangBat = true;
        }

        // Mũi tên xuống hoặc S: Chạy lùi
        dangLui = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);

        // A/D hoặc mũi tên trái/phải: rẽ
        dauVaoRe = 0f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) dauVaoRe += 1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) dauVaoRe -= 1f;

        // Space: nhảy
        if (Input.GetKeyDown(KeyCode.Space))
        {
            thoiDiemBamNhay = Time.time;
        }
    }

    private void FixedUpdate()
    {
        rb.angularVelocity = Vector3.zero;

        DiChuyenXe();
        ReXe();
        PhanhXe();
        NhayXe();
    }

    public void DiChuyenXe()
    {
        float tocDoMucTieu = 0f;

        if (dangTangToc)
        {
            tocDoMucTieu = tocDoXe * heSoTangToc;
        }
        else if (dangLui)
        {
            // Cho phép chạy lùi với tốc độ bằng một nửa tốc độ tiến
            tocDoMucTieu = -tocDoXe * 0.5f;
        }
        else if (xeDangBat)
        {
            tocDoMucTieu = tocDoXe;
        }

        tocDoDangChay = Mathf.Lerp(tocDoDangChay, tocDoMucTieu, giaToc * Time.fixedDeltaTime);

        Vector3 huongDiChuyen = transform.right * tocDoDangChay;
        rb.linearVelocity = new Vector3(huongDiChuyen.x, rb.linearVelocity.y, huongDiChuyen.z);
    }

    public void ReXe()
    {
        if (Mathf.Abs(tocDoDangChay) < 0.1f || dauVaoRe == 0f) return;

        // Nếu đang lùi, đảo chiều hướng rẽ lại cho tự nhiên
        float huongReThucTe = (tocDoDangChay < 0) ? -dauVaoRe : dauVaoRe;

        // Đã sửa lại lỗi dính chữ và thiếu dấu * ở đây
        Quaternion re = Quaternion.Euler(0f, huongReThucTe * lucReXe * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * re);
    }

    public void PhanhXe()
    {
        if (hieuUngPhanh != null)
        {
            hieuUngPhanh.SetActive(dangLui && Mathf.Abs(tocDoDangChay) > 1f);
        }
    }

    private void NhayXe()
    {
        bool duocBamGanDay = Time.time - thoiDiemBamNhay <= thoiGianDemNhay;
        bool duocChamDatGanDay = Time.time - thoiDiemChamDat <= thoiGianCoyote;

        if (!duocBamGanDay || !duocChamDatGanDay) return;

        thoiDiemBamNhay = -10f;
        thoiDiemChamDat = -10f;

        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * lucNhay, ForceMode.Impulse);
    }
}