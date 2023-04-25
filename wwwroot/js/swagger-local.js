window.swaggerUI = {
    load: function (specUrl) {    
        const ui = SwaggerUIBundle({
            url: specUrl,
            domNode: document.getElementById("swagger-ui"),
            presets: [
                SwaggerUIBundle.presets.apis,
                SwaggerUIStandalonePreset
            ]
        })
        window.ui = ui
    },
    loadContent: function (specObj) {
        var myJSON = JSON.parse(specObj);        
       // alert(specObj);
        const ui = SwaggerUIBundle({            
            url: null,
            domNode: document.getElementById("swagger-ui"),
            spec: myJSON,
            presets: [
                SwaggerUIBundle.presets.apis,
                SwaggerUIStandalonePreset
            ]
        })
        window.ui = ui
    }
}

window.swaggerEditor = {
    load: function (specUrl) {
        const editor = SwaggerEditorBundle({
            url: specUrl,
            domNode: document.getElementById("swagger-editor"),              
            presets: [
                SwaggerEditorStandalonePreset
            ]
        })
        window.editor = editor 
    },
    loadContent: function (specObj) {
        var myJSON = JSON.parse(specObj);
        // alert(specObj);
        const editor = SwaggerEditorBundle({
            url: null,
            domNode: document.getElementById("swagger-editor"),
            spec: myJSON,
            presets: [
                SwaggerEditorStandalonePreset
            ]
        })
        window.editor = editor
    }
}