function GetAllUsers() {
    $.ajax({
        url: "/Home/GetAllUsers",
        method: "GET",
        success: function (data) {

           
            let subContent = '';
            console.log("heloooo")
            for (let i = 0; i < data.length; i++) {
                let style = '';

                var usersListDiv = document.getElementById("users-list");
                var classList= (data[i].isOnline) ? "status-online" : "status-offline"; 
                subContent = `<button class='btn btn-outline-primary' onclick="SendFollow('${data[i].id}')">Follow</button>`;
                var content = "";
                     

                let item = `
             <div class='col-lg-3 col-sm-6'>
                <div class='single-friends-card'>
                        <div class='friends-image'>
                            <a href='#'>
                                <img src='~/assets/images/friends/friends-bg-1.jpg' alt='image'>
                            </a>
                            <div class='icon'>
                                <a href='#'><i class='flaticon-user'></i></a>
                            </div>
                        </div>
                        <div class='friends-content'>
                            <div class='friends-info d-flex justify-content-between align-items-center'>
                                <a href='#'>
                                    <img src='~/assets/images/friends/friends-1.jpg' alt='image'>
                                </a>
                                <div class='text ms-3'>
                                    <h3><a href='#'>${data[i].userName}</a></h3>
                                    <span>10 Mutual Friends</span>
                                </div>
                            </div>
                            <ul class='statistics'>
                                <li>
                                    <a href='#'>
                                        <span class='item-number'>862</span>
                                        <span class='item-text'>Likes</span>
                                    </a>
                                </li>
                                <li>
                                    <a href='#'>
                                        <span class='item-number'>91</span>
                                        <span class='item-text'>Following</span>
                                    </a>
                                </li>
                                <li>
                                    <a href='#'>
                                        <span class='item-number'>514</span>
                                        <span class='item-text'>Followers</span>
                                    </a>
                                </li>
                            </ul>
                            <div class='button-group d-flex justify-content-between align-items-center'>
                                <div class='add-friend-btn'>
                                   ${subContent}
                                </div>
                                <div class='send-message-btn'>
                                    <button type='submit'>Send Message</button>
                                </div>
                            </div>
                        </div>
                </div>
             </div>
                   `;
                content += item;
            }
            $("#users-list").html(content);
        }

    })

}

GetAllUsers();

//console.log("before everything")
//const connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:7195/statusHub").build();

//connection.start().then(function () {
//   console.log("connection start");
//})
//    .catch(function (err) {
//        return console.error(err.toString());
//    })


//connection.on("ReceiveUserStatus", function (userId, isOnline) {
//    if (userId === currentUserId) {
//        let currentUserSpan = document.querySelector(`#user-${userId}`);
//        console.log(currentUserSpan);
//        console.log("inside js");
//        if (currentUserSpan) {
//            if (isOnline) {
//                currentUserSpan.classList = "status-online"; 
//            } else {
//                currentUserSpan.classList = "status-offline"; 
//            }
//        }
//    }
//});

