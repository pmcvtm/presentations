const execSync = require('child_process').execSync;
const slideDir = process.argv[2] || 'DID-NOT-SPECIFY';

if(slideDir === 'DID-NOT-SPECIFY'){
    process.stdout.write("ERROR: You must specify a presentation to publish");
    process.exit();
}

execSync('cd slides/' + slideDir + ' && reveal-md readme.md --static ../../publish/' + slideDir + ' --static-dirs=assets', {stdio:[0, 1, 2]});
