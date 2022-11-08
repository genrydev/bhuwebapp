pipeline {
    agent none    
    stages {
        stage('Build, Test, Code Quality & Upload Artifact') {
            agent { label "windows" }
            environment {
                GIT_HASH = GIT_COMMIT.take(8)
                ARTIFACT_FILENAME = "webapp-${BUILD_NUMBER}-${env.GIT_HASH}.zip"
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
                        echo 'Unit Test'
                    }
                }
                stage ('Code Quality') {
                    steps {
                        echo 'Code Quality'
                    }
                }
                stage('Build Artifact for Upload') {
                    steps {
                        echo 'Build Artifact for Upload'
                    }
                }
                stage ('Upload Artifact'){
                    steps {
                        //echo "${env.WORKSPACE}"
                        //echo "${env.GIT_HASH}"
                        zip zipFile: "${ARTIFACT_FILENAME}", archive: false, dir: "bhuwebapp/bin/app.publish"
                        nexusArtifactUploader (
                            nexusVersion: 'nexus3',
                            protocol: 'http',
                            nexusUrl: 'supervm.eastus.cloudapp.azure.com:8081',
                            groupId: 'bhu.webapps.bhu-main-web-app',
                            version: 'snapshot',
                            repository: 'files',
                            credentialsId: 'nexus-admin',
                            artifacts: [
                                [artifactId: "${ARTIFACT_FILENAME}",
                                type: 'zip',
                                classifier: '1.0.0',
                                file: "${ARTIFACT_FILENAME}"]
                            ]
                        )
                    }
                }
            }
        }
    }
    // post {
    //     always {
    //         cleanWs()
    //     }
    // }
}