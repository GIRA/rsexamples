(ns rsteam.core
  (:require [clojure.core.async :as a :refer [go <!]]
            [rsproxy.server :as s]
            [rsteam.snapshot :as snap]
            [rsteam.strategy :as strategy]))

(defonce server (atom (s/make-server)))

(defn format-response [snapshot robot-idx known-ball?]
  (let [{:keys [ball robots]} snapshot
        robot (nth robots robot-idx)
        [vl vr] (:wheels robot)]
    {:team [(when known-ball?
              (select-keys ball [:x :y]))]
     :L vl
     :R vr}))

(defn loop* [snapshot]
  (let [robot-idx (-> snapshot :robot :index)
        known-ball? (-> snapshot :ball)]
    (-> snapshot
        (snap/transform)
        (strategy/run robot-idx)
        (format-response robot-idx known-ball?))))

(defn setup* []
  (print "SETUP!"))

(defn main [& [port]]
  (s/start! server (or port "12345")
            #'setup* #'loop*))

(defn ^:dev/before-load-async reload-begin* [done]
  (go (print "Stopping...")
      (<! (s/stop! server))
      (done)))

(defn ^:dev/after-load-async reload-end* [done]
  (go (print "Starting...")
      (<! (s/start! server))
      (done)))