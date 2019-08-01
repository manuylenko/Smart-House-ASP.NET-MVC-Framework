
        $(document).ready(function () {
            GetAllBooks();
            $("#addBook").click(function (event) {
                event.preventDefault();
                AddBook();
            });

        });
        function EditBook(tv, command) {
            switch (command)
            {
                case "TurnOffOn":
                    tv.OnOff = !(tv.OnOff);
                    break;
                case "VolumeMinus":
                    tv.Volume -= 10;
                    break;
                case "VolumePlus":
                    tv.Volume += 10;
                    break;
                case "SoundOnOff":
                    if (tv.Volume != 0)
                    {
                        tv.CurrentVolume = tv.Volume;
                        tv.Volume = 0;
                    }
                    else
                    {
                        tv.Volume = tv.CurrentVolume;
                    }
                    break;
                case "PreviousMode":
                    if (tv.Channel > 0 && tv.OnOff)
                    {                      
                        tv.Channel -= 1;
                    }
                    break;
                case "NextMode":
                    if (tv.Channel < 4 && tv.OnOff)
                    {
                        tv.Channel +=1;
                    }
                    break;
            }
            $.ajax({
                url: '/api/Tvs/' + tv.Id,
                type: 'PUT',
                data: JSON.stringify(tv),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    GetAllBooks();
                }
            });
        }
        function GetBook(id, command) {
            $.ajax({
                url: '/api/Tvs/' + id,
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    EditBook(data, command);
                },
                error: function (x, y, z) {
                    alert(x + '\n' + y + '\n' + z);
                }
            });
        }
        function EditItem(el) {
            var id = $(el).attr('data-item');
            var command = $(el).attr('id');
            GetBook(id, command);
        }
        function AddBook() {
            var book = {
                Img: "/Content/Images/tv.png",
                OnOff: false,
                ChannelsId: 0,
                Volume: 0,
                CurrentVolume: 0
            };
            $.ajax({
                url: '/api/Tvs',
                type: 'POST',
                data: JSON.stringify(book),
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    GetAllBooks();
                },
                error: function (x, y, z) {
                    alert(x + '\n' + y + '\n' + z);
                }
            });
        }
        function GetAllBooks() {
            $.ajax({
                url: '/api/Tvs',
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    WriteResponse(data);
                },
                error: function (x, y, z) {
                    alert(x + '\n' + y + '\n' + z);
                }
            });
        }
        function WriteResponse(books) {
            var strResult = "";
            $.each(books, function (index, book) {
                strResult += "<div class='form-device'><a href='/Devices/Create/Illuminator'>" +
                 "<img src='" + book.Img + " ' class='image-device' /></a>" +
                 "<a data-item='" + book.Id + "' onclick='DeleteItem(this);' >" +
                "<img src='/Content/Images/delete.png' class='button-remove-device' /></a>" +                                       
                "<div class='device-field'>" +
                "<span>OnOff: " + book.OnOff + "</span>" +
                "<span>ChannelsId: " + book.ChannelsId + "</span>" +
                "<span>Volume: " + book.Volume + "</span></div>" +
                                "<a id='TurnOffOn' data-item='" + book.Id + "' onclick='EditItem(this);' >" +
                "<img src='/Content/Images/power.png' class='button-device' /></a>" +
                                "<a id='VolumeMinus' data-item='" + book.Id + "' onclick='EditItem(this);' >" +
                "<img src='/Content/Images/power.png' class='button-device' /></a>" +
                                "<a id='VolumePlus' data-item='" + book.Id + "' onclick='EditItem(this);' >" +
                "<img src='/Content/Images/power.png' class='button-device' /></a>" +
                                "<a id='SoundOnOff' data-item='" + book.Id + "' onclick='EditItem(this);' >" +
                "<img src='/Content/Images/power.png' class='button-device' /></a>" +
                                "<a id='PreviousMode' data-item='" + book.Id + "' onclick='EditItem(this);' >" +
                "<img src='/Content/Images/power.png' class='button-device' /></a>" +
                                "<a id='NextMode' data-item='" + book.Id + "' onclick='EditItem(this);' >" +
                "<img src='/Content/Images/power.png' class='button-device' /></a></div>";
            });
            $("#tableBlock").html(strResult);
        }
        function DeleteBook(id) {
            $.ajax({
                url: '/api/Tvs/' + id,
                type: 'DELETE',
                contentType: "application/json;charset=utf-8",
                success: function (data) {
                    GetAllBooks();
                },
                error: function (x, y, z) {
                    alert(x + '\n' + y + '\n' + z);
                }
            });
        }
        function DeleteItem(el) {
            var id = $(el).attr('data-item');
            DeleteBook(id);
        }



