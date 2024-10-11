﻿"use strict"
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
                var status = (data[i].isOnline) ? "online" : "offline";
                if (data[i].hasRequestPending) {
                    subContent = `<button class='btn btn-outline-secondary' onclick="TakeRequest('${data[i].id}')">Already Sent</button>`;
                }
                else {
                    if (data[i].isFriend) {
                        subContent = `<button class='btn btn-outline-secondary' onclick="UnfollowRequest('${data[i].id}')">UnFollow</button>`;
                        //<a class='btn btn-outline-secondary m-2' href='/Home/GoChat/${data[i].id}' >Go Chat</a>

                    }
                    else {
                        subContent = `<button class='btn btn-outline-primary' onclick="SendFollow('${data[i].id}')">Follow</button>`;
                    }
                }
               
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
                                    <img src=${data[i].profileImageUrl} alt='image'>
                                </a>
                                <div class='text ms-3'>
                                    <h3><a href='#'>${data[i].userName}</a></h3>
                                    <span>${status}</span>
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


function SendFollow(id) {
    const element = document.querySelector("#alert");
    element.style.display = "none";
    $.ajax({
        url: `/Home/SendFollow/${id}`,
        method: "GET",
        success: function (data) {
            element.style.display = "block";
            element.innerHTML = "Your friend request sent successfully";
            GetAllUsers();
            SendFollowCall(id);
            setTimeout(() => {
                element.innerHTML = "";
                element.style.display = "none";
            }, 5000)
        }
    })
}










function toggleLike(postId) {
    $.ajax({
        url: '/Home/ToggleLike',  
        type: 'POST',
        data: { postId: postId },
        success: function (response) {
            if (response.success) {
                var likeButton = $('.like-button[data-post-id="' + postId + '"]');
                var likeCountSpan = likeButton.find('.number');

             
                likeCountSpan.text(response.likeCount);

             
                if (response.isLiked) {
                    likeButton.addClass('liked');
                    likeButton.find('span:first').text('Liked');
                } else {
                    likeButton.removeClass('liked');
                    likeButton.find('span:first').text('Like');
                }
            } else {
                console.log("An error occurred while toggling the like");
            }
        },
        error: function (xhr, status, error) {
            console.error("Error: ", error);
        }
    });
}




 


function addNewPost() {


    $.ajax({
        url: "/Home/GetCreatePostFormViewComponent",
        type: "GET",
        success: function (component) {
            var postArea = document.getElementById('postArea');
            postArea.innerHTML = "";
            postArea.innerHTML += component;




            $.ajax({
                url: "/Home/GetPosts",
                type: "GET",
                success: function (posts) {
                    var postHtml = "";
                    for (let i = 0; i < posts.length; i++) {
                        console.log(posts[i].id);
                        postHtml += `
        <div class="news-feed news-feed-post">
            <div class="post-header d-flex justify-content-between align-items-center">
                <div class="image">
                    <a href="my-profile.html"><img src={${posts[i].user.userProfileImage}} class="rounded-circle" alt="image"></a>
                </div>
                <div class="info ms-3">
                    <span class="name"><a href="my-profile.html">${posts[i].user.userName}</a></span>
                    <span class="small-text"><a href="#">${posts[i].timeAgo}</a></span>
                </div>
${posts[i].isCurrentUser ? `                <div class="dropdown">
                    <button class="dropdown-toggle" type="button" data-bs-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="flaticon-menu"></i></button>
                    <ul class="dropdown-menu">
                        <li><a class="dropdown-item d-flex align-items-center" href="#"><i class="flaticon-edit"></i> Edit Post</a></li>
                        <li><a class="dropdown-item d-flex align-items-center" href="#"><i class="flaticon-trash"></i> Delete Post</a></li>
                    </ul>
                </div>`: ''}
            </div>

            <div class="post-body">
                <p>${posts[i].message}</p>
            ${posts[i].postImage ? `
            <div class="post-image">
                <img src="${posts[i].postImage}" alt="image">
            </div>` : ''}
                <ul class="post-meta-wrap d-flex justify-content-between align-items-center">
                    <li class="post-react">
                   <a href="javascript:void(0)" class="like-button ${posts[i].isLiked ? 'liked' : ''}" data-post-id="${posts[i].postId}" onclick="toggleLike(${posts[i].postId})">
    <i class="${posts[i].isLiked ? 'flaticon-liked' : 'flaticon-like'}"></i><span>${posts[i].isLiked ? 'Liked' : 'Like'}</span>
    <span class="number">${posts[i].likeCount}</span>
</a>
                    </li>
                    <li class="post-comment">
                        <a href="#"><i class="flaticon-comment"></i><span>Comment</span> <span class="number">0 </span></a>
                    </li>
                    <li class="post-share">
                        <a href="#"><i class="flaticon-share"></i><span>Share</span> <span class="number">0 </span></a>
                    </li>
                </ul>
                <div class="post-comment-list" id="commentsSection_${posts[i].postId}">
                    ${getRecentComments(posts[i].postId)}
                </div>

                <form class="post-footer" >
                    <div class="footer-image">
                        <a href="#"><img src="assets/images/user/user-1.jpg" class="rounded-circle" alt="image"></a>
                    </div>
                    <div class="form-group">
                        <textarea id="commentText_${posts[i].postId}" class="form-control" placeholder="Write a comment..."></textarea>
                        <label><a href="#"><i class="flaticon-photo-camera"></i></a></label>
                    </div>
                    <button type="button" onclick="submitComment(${posts[i].id})" class="btn btn-primary">Submit</button>
                </form>
            </div>
        </div>`;
                    }
                    postArea.innerHTML += postHtml;
                }

            });
        }
    });
}

//addNewPost()

function getRecentComments(postId) {
    var commentsHtml = "";
    $.ajax({
        url: "/Home/GetRecentComments", 
        type: "GET",
        data: { postId: postId },
        success: function (comments) {
            for (let j = 0; j < comments.length; j++) {
                commentsHtml += `
                <div class="comment-list">
                    <div class="comment-image">
                        <a href="my-profile.html"><img src="${comments[j].user.userProfileImage}" class="rounded-circle" alt="image"></a>
                    </div>
                    <div class="comment-info">
                        <h3><a href="my-profile.html">${comments[j].user.userName}</a></h3>
                        <span>${comments[j].commentedAt}</span>
                        <p>${comments[j].commentText}</p>
                        <ul class="comment-react">
                           
                            <li><a href="#">Reply</a></li>
                        </ul>
                    </div>
                </div>`;
            }
        }
    });
    return commentsHtml;
}

function submitComment(postId) {
    var commentText = document.getElementById(`commentText_${postId}`);
    console.log("inside submit comments")
    $.ajax({
        url: '/Home/AddComment',
        type: 'POST',
        data: {
            PostId: postId,
            CommentText: commentText.value
        },
        success: function (comments) {
            document.getElementById(`commentInput-${postId}`).value = "";

            var commentArea = document.getElementById(`comments-${postId}`);
            var commentHtml = "";
            for (let i = 0; i < comments.length; i++) {
                commentHtml += `
                <div class="comment-list">
                    <div class="comment-image">
                        <a href="my-profile.html"><img src="${comments[i].userProfileImage}" class="rounded-circle" alt="image"></a>
                    </div>
                    <div class="comment-info">
                        <h3><a href="my-profile.html">${comments[i].userName}</a></h3>
                        <span>${comments[i].timeAgo}</span>
                        <p>${comments[i].commentText}</p>
                        <ul class="comment-react">
                            <li><a href="#" class="like">Like(${comments[i].likeCount})</a></li>
                            <li><a href="#">Reply</a></li>
                        </ul>
                    </div>
                </div>`;
            }
            commentArea.innerHTML = commentHtml;
        },
        error: function () {
            alert("Failed to add comment.");
        }
    });
}


function submitPost() {
    var formData = new FormData(document.getElementById('postForm'));

    $.ajax({
        url: '/Home/CreatePost',
        type: 'POST',
        processData: false,
        contentType: false,
        data: formData,
        success: function (response) {
            if (response.success) {
                //addNewPost(response);
                $('#postForm')[0].reset();
                console.log(response.message);
            } else {
                console.log("Error occurred");
            }

            addNewPost();

        },
        error: function (xhr, status, error) {

            console.error(error);
        }
    });
}
