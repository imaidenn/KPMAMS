<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoMeet.aspx.cs" Inherits="KPMAMS.VideoMeet" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Meeting</title>
    <link type="text/css" rel="stylesheet" href="~/css/meeting.css" />
    <meta name="format-detection" content="telephone=no">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <meta http-equiv="origin-trial" content="">
    <script type="text/javascript" src="https://source.zoom.us/2.0.1/lib/av/../webim.min.js"></script>
    <script type="text/javascript" src="https://source.zoom.us/2.0.1/lib/av/1504_js_media.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-default">
               <div class="panel-heading">Meeting Info</div>
               <div class="panel-body">

        <div>
            <div id="zmmtg-root">
                <div class="root-inner">
                    <div id="content_container" class="main-content" style="background: white; height: 969px; display: flex; justify-content: center; align-items: center;">
                        <div id="content">
                            <div id="notice-slot"></div>
                            <div id="imgroup-guide-notice-slot"><imgroup-notice v-show="notice.isShowImgroupNotice"></imgroup-notice></div>
                            <div class="container-preview" role="main" aria-label="main content">
                                <div class="form-container">

                                        <input type="hidden" id="meeting_number" value="">
                                        <input type="hidden" id="track_id" value="">
                                        <input type="hidden" id="jmf_code" value="">
                                        <input type="hidden" id="meeting_result" value="">
                                        <input type="hidden" id="tk" value="">
                                        <input type="hidden" id="refTK" value="">
                                        <input type="hidden" id="cdn_patch" value="">
                                        <input type="hidden" id="wpk" value="">
                                        <input type="hidden" id="from" value="">
                                        <input type="hidden" id="previewOptions" value="7">

 </div>
                                <div class="media-preview-container">
                                    <p><h1 class="meeting-title">Join Meeting</h1></p>
                                    <div class="align-middle av-preview-container" style="width: 699px; height: 393px;">
                                        <div id="media-preview-errorMsg-container" role="status" aria-live="assertive" aria-labelledby="media-preview-errorMsg-content" style="display: flex;">
                                            <div id="media-preview-errorMsg-icon" class="media-preview-icon" style="display: inline;"></div>
                                            <label id="media-preview-errorMsg-text-container">
                                                <span id="media-preview-errorMsg-content">Unable to detect a camera or microphone. Please check your device and try again.</span>
                                                <a id="media-preview-errorMsg-link" target="_blank" href="https://zoom.us/wc/support/mic" style="display: none;">Learn more</a></label></div>
                                        <video id="media-preview-camera-on" playsinline="" data-video="0" style="display: none;"></video>
                                        <div id="media-preview-avatar" class="media-preview-icon media-preview-avatar-default media-preview-avatar-signed-out" style="display: none;"></div>
                                        <div id="media-preview-tooltip-container" style="display: block;">
                                            <div class="media-preview-tooltip-content-container">
                                                <div class="media-preview-tooltip-content" role="dialog" aria-labelledby="tooltip-text">
                                                    <span id="tooltip-text" class="media-preview-tooltip-text">The audio and video preferences you select here will be used for future meetings.</span>
                                                    <button type="button" id="media-preview-tooltip-btn">Got it</button></div></div>
                                            <div class="media-preview-tooltip-pointer"></div></div><div class="media-preview-control">
                                                <div class="media-preview-control-btn-container media-preview-audio-mic-btn-container disable-hover">
                                                    <button type="button" class="media-control-btn media-preview-icon-container" id="mic-btn" aria-label="microphone on" aria-pressed="true" disabled="" style="display: inline-block;">
                                                        <div id="mic-icon" class="media-preview-icon media-preview-icon-mic-on media-preview-icon-mic-warning"></div></button>
                                                    <button type="button" class="media-control-btn media-preview-icon-container" id="mic-option-btn" aria-label="toggle microphone option menu" aria-expanded="false" disabled="" style="display: inline-block; background-color: transparent;">
                                                        <div id="mic-option-icon" class="media-preview-icon media-preview-icon-mic-option"></div></button>
                                                    <div id="media-preview-mic-option-lists" style="display: none;">
                                                        <button type="button" id="media-preview-disconnect-audio" class="media-preview-mic-option-btn">
                                                            <span class="media-preview-disconnect-audio-text">Leave Computer Audio</span></button></div>
                                                    <button type="button" class="media-control-btn media-preview-icon-container" id="audio-btn" aria-label="toggle audio connection" style="display: none;">
                                                        <div id="audio-icon" class="media-preview-icon media-preview-icon-audio-disconnected"></div></button></div>
                                                <div class="media-preview-control-btn-container media-preview-video-btn-container disable-hover">
                                                    <button type="button" class="media-control-btn media-preview-icon-container" id="video-btn" aria-pressed="true" aria-label="video on" disabled="">
                                                        <div id="video-icon" class="media-preview-icon media-preview-icon-video-on media-preview-icon-video-warning"></div></button></div></div></div>
                                    <div class="mini-layout-body" style="margin: 50px auto auto; padding-bottom: 0px;"><div class="form-group"><div class="controls">
                                        <button tabindex="0" type="button" class="zm-btn joinWindowBtn btn btn-primary btn-block btn-lg submit zm-btn--default zm-btn__outline--blue" aria-label="" runat="server" >Join<span class="loading" style="display: none;"></span></button></div></div></div></div></div></div></div></div></div>

            <div id="aria-notify-area" aria-atomic="true" aria-live="assertive"></div>

            <script src="Scripts/tool.js"></script>
            <script src="Scripts/vconsole.min.js"></script>
            <script src="Scripts/meeting.js"></script>


            <script src="https://source.zoom.us/2.0.1/lib/vendor/react.min.js"></script>
            <script src="https://source.zoom.us/2.0.1/lib/vendor/react-dom.min.js"></script>
            <script src="https://source.zoom.us/2.0.1/lib/vendor/redux.min.js"></script>
            <script src="https://source.zoom.us/2.0.1/lib/vendor/redux-thunk.min.js"></script>
            <script src="https://source.zoom.us/2.0.1/lib/vendor/lodash.min.js"></script>
            <script src="https://source.zoom.us/zoom-meeting-2.0.1.min.js"></script>
        </div>
                   </div>
            </div>

    </form>
</body>
</html>
