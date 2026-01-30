flowchart LR
  Session["Learning Session
  - start / stop
  - session state"]

  Activity["Activity
  - type
  - duration"]

  Material["Material
  - title
  - level"]

  Artifact["Artifact
  - notes
  - outputs"]


  Frontend --> Session
  Frontend --> Activity
  Frontend --> Material
  Frontend --> Artifact
