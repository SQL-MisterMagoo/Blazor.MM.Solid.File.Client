window.SolidFileClientDotNet = {
    fetchFoaf: function (url, name) {
        const fc = SolidFileClient;
        return fc.fetchAndParse(url).then(kb => {
            var subj = kb.sym(url);
            var obj = kb.sym("http://xmlns.com/foaf/0.1/" + name);
            names = kb.each(subj, obj);
            console.log('JSNAMES1:'+names);
            return names[0].value;
        }, err => console.log(err));
        
    }
};