$(document).ready(function () {
    //datatable
    $('#articlesTable').DataTable({
        language: {
            "sDecimal": ",",
            "sEmptyTable": "Tabloda herhangi bir veri mevcut değil",
            "sInfo": "_TOTAL_ kayıttan _START_ - _END_ arasındaki kayıtlar gösteriliyor",
            "sInfoEmpty": "Kayıt yok",
            "sInfoFiltered": "(_MAX_ kayıt içerisinden bulunan)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Sayfada _MENU_ kayıt göster",
            "sLoadingRecords": "Yükleniyor...",
            "sProcessing": "İşleniyor...",
            "sSearch": "Ara:",
            "sZeroRecords": "Eşleşen kayıt bulunamadı",
            "oPaginate": {
                "sFirst": "İlk",
                "sLast": "Son",
                "sNext": "Sonraki",
                "sPrevious": "Önceki"
            },
            "oAria": {
                "sSortAscending": ": artan sütun sıralamasını aktifleştir",
                "sSortDescending": ": azalan sütun sıralamasını aktifleştir"
            },
            "select": {
                "rows": {
                    "_": "%d kayıt seçildi",
                    "0": "",
                    "1": "1 kayıt seçildi"
                }
            }
        },
        "order":[[4,"desc"]]

    });
    //datatable

    //Chart.js
    $.get('/Admin/Article/GetAllByViewCount/?isAscending=false&takeSize=10',
        function (data) {
            const articleResult = jQuery.parseJSON(data);
            console.log(articleResult);
            let viewCountContext = $('#viewCountChart');
            let names = [];
            let vc = [];
            let cc = [];
            $.each(articleResult.$values, function (index, art) {
                const values = getJsonNetObject(art, articleResult.$values);
                names.push(values.Title);
                vc.push(values.ViewsCount);
                cc.push(values.CommentCount);
            });
            console.log(names);
            let viewCountChart = new Chart(viewCountContext,
                {
                    type: 'bar',
                    data: {
                        labels: names,
                        datasets: [
                            {
                                label: 'Okunma Sayısı',
                                data: vc,
                                backgroundColor: '#fb3640',
                                hoverBorderWidth: 4,
                                hoverBorderColor: 'black'
                            },
                            {
                                label: 'Yorum Sayısı',
                                data: cc,
                                backgroundColor: '#fdca40',
                                hoverBorderWidth: 4,
                                hoverBorderColor: 'black'
                            }]
                    },
                    options: {
                        plugins: {
                            legend: {
                                labels: {
                                    font: {
                                        size: 18
                                    }
                                }
                            }
                        }
                    }
                });
        });
});