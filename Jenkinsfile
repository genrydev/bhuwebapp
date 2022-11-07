pipeline {
    agent { label "windows" }
    environment {
        GIT_HASH = GIT_COMMIT.take(8)
    }
    stages {
        stage('Descargar dependencias') {
            steps {
                powershell 'nuget restore'
            }
        }
        stage('Build') {
            steps {
                powershell 'msbuild /verbosity:quiet /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=./FolderProfile.pubxml'
            }
        }
        stage ('Unit Test') {
            steps {
                powershell 'vstest.console ./bhuwebapp.Tests/bin/Release/bhuwebapp.Tests.dll /Settings:./test.runsettings'
            }
        }
        stage ('Code Quality') {
            steps {
                script {
                    def sqScannerMsBuildHome = tool 'SonarMSBuild'
                    withSonarQubeEnv('sonarqube') {
                        bat "${sqScannerMsBuildHome}\\SonarQube.Scanner.MSBuild.exe begin /k:bhuwebapp"
                        bat 'MSBuild.exe /t:Rebuild'
                        bat "${sqScannerMsBuildHome}\\SonarQube.Scanner.MSBuild.exe end"
                    }
                }
            }
        }
        stage ('Upload Artifact'){
            steps {
                //echo "${env.WORKSPACE}"
                echo "${env.GIT_HASH}"
                zip zipFile: "webapp-${env.GIT_HASH}.zip", archive: false, dir: "bhuwebapp/bin/app.publish"
                nexusArtifactUploader (
                    nexusVersion: 'nexus3',
                    protocol: 'http',
                    nexusUrl: 'supervm.eastus.cloudapp.azure.com:8081',
                    groupId: 'bhu.webapps',
                    version: '1.0.0',
                    repository: 'files',
                    credentialsId: 'nexus-admin',
                    artifacts: [
                        [artifactId: "web-app-1-${env.GIT_HASH}",
                        type: 'zip',
                        classifier: 'snapshot',
                        file: "webapp-${env.GIT_HASH}.zip"]
                    ]
                )
            }
        }
        stage ('Deploy To IIS Dev') {
            steps {
                echo 'Deploy To IIS Dev'
            }
        }
        stage ('Deploy Notification') {
            steps {
                echo 'Deploy Notification'
            }
        }
        stage ('Site Speed Test') {
            steps {
                echo 'Site Speed Test'
            }
        }
    }
    // post {
    //     always {
    //         cleanWs()
    //     }
    // }
}